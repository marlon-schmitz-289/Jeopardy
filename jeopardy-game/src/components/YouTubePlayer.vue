﻿<template>
  <div class="youtube-player-container">
    <div v-if="!isValidVideoId" class="error-message">
      Invalid YouTube URL or Video ID
    </div>

    <div v-else class="youtube-player">
      <div :id="playerId" class="player-element"></div>

      <div class="video-controls" v-if="showControls">
        <button @click="playVideo" class="control-btn play-btn">▶ Play</button>
        <button @click="pauseVideo" class="control-btn pause-btn">⏸ Pause</button>
        <button @click="stopVideo" class="control-btn stop-btn">⏹ Stop</button>
        <button @click="toggleMute" class="control-btn mute-btn">
          {{ isMuted ? '🔊' : '🔇' }} {{ isMuted ? 'Unmute' : 'Mute' }}
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onUnmounted, watch } from 'vue'
import { extractYouTubeVideoId } from '@/types/game'

declare global {
  interface Window {
    YT: any
    onYouTubeIframeAPIReady: () => void
  }
}

interface Props {
  videoUrl: string
  width?: string | number
  height?: string | number
  autoplay?: boolean
  controls?: boolean
  showCustomControls?: boolean
  muted?: boolean
}

interface Emits {
  (e: 'ready'): void
  (e: 'play'): void
  (e: 'pause'): void
  (e: 'end'): void
  (e: 'error', error: string): void
}

const props = withDefaults(defineProps<Props>(), {
  width: '100%',
  height: '315',
  autoplay: false,
  controls: true,
  showCustomControls: false,
  muted: false
})

const emit = defineEmits<Emits>()

const playerId = `youtube-player-${Math.random().toString(36).substr(2, 9)}`
const player = ref<any>(null)
const isPlayerReady = ref<boolean>(false)
const isMuted = ref<boolean>(props.muted)
const checkInterval = ref<number | null>(null)
const showControls = computed(() => props.showCustomControls)

// Parse timing settings embedded in the video URL as query params (t, length, muted)
const urlSettings = computed(() => {
  try {
    const url = new URL(props.videoUrl)
    return {
      startTime: parseInt(url.searchParams.get('t') || '0') || 0,
      length: parseInt(url.searchParams.get('length') || '0') || 0,
      muted: url.searchParams.get('muted') === '1'
    }
  } catch {
    return { startTime: 0, length: 0, muted: false }
  }
})

const effectiveStartTime = computed(() => urlSettings.value.startTime)
const effectiveEndTime = computed(() =>
  urlSettings.value.length > 0 ? urlSettings.value.startTime + urlSettings.value.length : 0
)
const effectiveMuted = computed(() => urlSettings.value.muted || props.muted)

const videoId = computed(() => {
  return extractYouTubeVideoId(props.videoUrl)
})

const isValidVideoId = computed(() => {
  return videoId.value !== null
})

const loadYouTubeAPI = (): Promise<void> => {
  return new Promise((resolve) => {
    if (window.YT && window.YT.Player) {
      resolve()
      return
    }

    const existingScript = document.querySelector('script[src="https://www.youtube.com/iframe_api"]')
    if (existingScript) {
      // Script already injected — poll until ready
      const checkReady = setInterval(() => {
        if (window.YT && window.YT.Player) {
          clearInterval(checkReady)
          resolve()
        }
      }, 100)
      return
    }

    const tag = document.createElement('script')
    tag.src = 'https://www.youtube.com/iframe_api'
    const firstScriptTag = document.getElementsByTagName('script')[0]
    firstScriptTag.parentNode?.insertBefore(tag, firstScriptTag)

    // Chain onto any existing callback so multiple instances don't clobber each other
    const previousCallback = window.onYouTubeIframeAPIReady
    window.onYouTubeIframeAPIReady = () => {
      if (previousCallback) previousCallback()
      resolve()
    }
  })
}

const initPlayer = async () => {
  if (!videoId.value) return

  await loadYouTubeAPI()

  player.value = new window.YT.Player(playerId, {
    height: props.height,
    width: props.width,
    videoId: videoId.value,
    playerVars: {
      autoplay: props.autoplay ? 1 : 0,
      controls: props.controls ? 1 : 0,
      start: effectiveStartTime.value || 0,
      mute: effectiveMuted.value ? 1 : 0,
      rel: 0,
      modestbranding: 1
    },
    events: {
      onReady: onPlayerReady,
      onStateChange: onPlayerStateChange,
      onError: onPlayerError
    }
  })
}

const onPlayerReady = (event: any) => {
  isPlayerReady.value = true

  if (effectiveStartTime.value > 0) {
    event.target.seekTo(effectiveStartTime.value, true)
  }

  if (effectiveEndTime.value > 0) {
    startEndTimeCheck()
  }

  emit('ready')
}

const onPlayerStateChange = (event: any) => {
  switch (event.data) {
    case window.YT.PlayerState.ENDED:
      emit('end')
      stopEndTimeCheck()
      break
    case window.YT.PlayerState.PLAYING:
      emit('play')
      if (effectiveEndTime.value > 0) {
        startEndTimeCheck()
      }
      break
    case window.YT.PlayerState.PAUSED:
      emit('pause')
      stopEndTimeCheck()
      break
  }
}

const onPlayerError = (event: any) => {
  emit('error', `YouTube player error: ${event.data}`)
}

const startEndTimeCheck = () => {
  stopEndTimeCheck()

  if (effectiveEndTime.value <= 0) return

  checkInterval.value = window.setInterval(() => {
    if (player.value && player.value.getCurrentTime) {
      const currentTime = player.value.getCurrentTime()
      if (currentTime >= effectiveEndTime.value) {
        stopEndTimeCheck()
        player.value.pauseVideo()
        player.value.seekTo(effectiveStartTime.value, true)
        emit('end')
      }
    }
  }, 250)
}

const stopEndTimeCheck = () => {
  if (checkInterval.value) {
    clearInterval(checkInterval.value)
    checkInterval.value = null
  }
}

const playVideo = (): void => {
  if (player.value && player.value.playVideo) {
    player.value.playVideo()
  }
}

const pauseVideo = (): void => {
  if (player.value && player.value.pauseVideo) {
    player.value.pauseVideo()
  }
}

const stopVideo = (): void => {
  if (player.value && player.value.stopVideo) {
    player.value.stopVideo()
  }
}

const toggleMute = (): void => {
  if (!player.value) return

  if (isMuted.value) {
    player.value.unMute()
  } else {
    player.value.mute()
  }
  isMuted.value = !isMuted.value
}

onMounted(() => {
  initPlayer()
})

onUnmounted(() => {
  stopEndTimeCheck()
  if (player.value && player.value.destroy) {
    player.value.destroy()
  }
})
</script>

<style scoped>
.youtube-player-container {
  width: 100%;
  max-width: 800px;
  margin: 0 auto;
}

.youtube-player {
  position: relative;
  width: 100%;
}

.youtube-player iframe,
.player-element {
  width: 100%;
  height: auto;
  min-height: 315px;
  border-radius: 8px;
}

.error-message {
  background: #f44336;
  color: white;
  padding: 20px;
  border-radius: 8px;
  text-align: center;
  font-family: 'JetBrains Mono', monospace;
  font-size: 16px;
}

.video-controls {
  display: flex;
  justify-content: center;
  gap: 10px;
  margin-top: 15px;
  flex-wrap: wrap;
}

.control-btn {
  background: #ff0000;
  color: white;
  border: none;
  padding: 10px 15px;
  border-radius: 5px;
  cursor: pointer;
  font-family: 'JetBrains Mono', monospace;
  font-size: 14px;
  transition: all 0.2s;
  display: flex;
  align-items: center;
  gap: 5px;
}

.control-btn:hover {
  background: #cc0000;
  transform: translateY(-2px);
}

.control-btn:active {
  transform: translateY(0);
}

/* Responsive design */
@media (max-width: 768px) {
  .youtube-player iframe {
    min-height: 250px;
  }

  .video-controls {
    gap: 5px;
  }

  .control-btn {
    padding: 8px 12px;
    font-size: 12px;
  }
}

@media (max-width: 480px) {
  .youtube-player iframe {
    min-height: 200px;
  }

  .control-btn {
    padding: 6px 10px;
    font-size: 11px;
  }
}
</style>
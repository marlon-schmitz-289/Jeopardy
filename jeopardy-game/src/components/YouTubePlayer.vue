<template>
  <div class="youtube-player-container">
    <div v-if="!isValidVideoId" class="error-message">
      Invalid YouTube URL or Video ID
    </div>

    <div v-else class="youtube-player">
      <iframe
          :src="embedUrl"
          :width="width"
          :height="height"
          frameborder="0"
          allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
          allowfullscreen
          @load="onPlayerReady"
      ></iframe>

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
import { ref, computed, onMounted, onUnmounted } from 'vue'
import { extractYouTubeVideoId } from '@/types/game'

interface Props {
  videoUrl: string
  width?: string | number
  height?: string | number
  autoplay?: boolean
  controls?: boolean
  showCustomControls?: boolean
  startTime?: number
  endTime?: number
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
  startTime: 0,
  muted: false
})

const emit = defineEmits<Emits>()

const isPlayerReady = ref<boolean>(false)
const isMuted = ref<boolean>(props.muted)
const showControls = computed(() => props.showCustomControls)

const videoId = computed(() => {
  return extractYouTubeVideoId(props.videoUrl)
})

const isValidVideoId = computed(() => {
  return videoId.value !== null
})

const embedUrl = computed(() => {
  if (!videoId.value) return ''

  const params = new URLSearchParams()
  params.set('enablejsapi', '1')
  params.set('origin', window.location.origin)

  if (!props.controls) params.set('controls', '0')
  if (props.autoplay) params.set('autoplay', '1')
  if (props.muted) params.set('mute', '1')
  if (props.startTime > 0) params.set('start', props.startTime.toString())
  if (props.endTime && props.endTime > 0) params.set('end', props.endTime.toString())

  // Disable related videos from other channels
  params.set('rel', '0')

  return `https://www.youtube.com/embed/${videoId.value}?${params.toString()}`
})

const playVideo = (): void => {
  postMessageToPlayer('playVideo')
  emit('play')
}

const pauseVideo = (): void => {
  postMessageToPlayer('pauseVideo')
  emit('pause')
}

const stopVideo = (): void => {
  postMessageToPlayer('stopVideo')
}

const toggleMute = (): void => {
  if (isMuted.value) {
    postMessageToPlayer('unMute')
  } else {
    postMessageToPlayer('mute')
  }
  isMuted.value = !isMuted.value
}

const postMessageToPlayer = (command: string, args?: any): void => {
  const iframe = document.querySelector('iframe') as HTMLIFrameElement
  if (iframe && iframe.contentWindow) {
    iframe.contentWindow.postMessage(
        JSON.stringify({
          event: 'command',
          func: command,
          args: args || []
        }),
        'https://www.youtube.com'
    )
  }
}

const onPlayerReady = (): void => {
  isPlayerReady.value = true
  emit('ready')
}

// Listen for YouTube player events
const handleYouTubeMessage = (event: MessageEvent): void => {
  if (event.origin !== 'https://www.youtube.com') return

  try {
    const data = JSON.parse(event.data)
    if (data.event === 'video-progress') {
      // Handle progress updates if needed
    } else if (data.event === 'onStateChange') {
      switch (data.info) {
        case 0: // ended
          emit('end')
          break
        case 1: // playing
          emit('play')
          break
        case 2: // paused
          emit('pause')
          break
      }
    }
  } catch (error) {
    // Ignore parsing errors
  }
}

onMounted(() => {
  window.addEventListener('message', handleYouTubeMessage)
})

onUnmounted(() => {
  window.removeEventListener('message', handleYouTubeMessage)
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

.youtube-player iframe {
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
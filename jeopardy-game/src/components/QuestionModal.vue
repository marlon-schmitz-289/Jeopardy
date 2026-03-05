<template>
  <div v-if="question" class="modal-overlay" @click="close">
    <div class="modal-content" @click.stop :class="{ 'video-modal': question.type === QuestionType.YouTube }">
      <div class="question-display">
        <div v-if="question.type === QuestionType.Text" class="text-question">
          {{ question.question }}
        </div>

        <img v-else-if="question.type === QuestionType.Image"
             :src="question.question"
             class="image-question"
             alt="Question Image" />

        <div v-else-if="question.type === QuestionType.Audio" class="audio-controls">
          <button @click="playAudio" class="play-btn">▶</button>
          <button @click="stopAudio" class="stop-btn">■</button>
          <span>{{ audioStatus }}</span>
        </div>

        <div v-else-if="question.type === QuestionType.YouTube" class="youtube-question">
          <YouTubePlayer
              :video-url="question.question"
              :autoplay="false"
              :controls="true"
              :show-custom-controls="true"
              width="100%"
              height="400"
              @ready="onVideoReady"
              @play="onVideoPlay"
              @pause="onVideoPause"
              @end="onVideoEnd"
              @error="onVideoError"
          />
          <div v-if="videoError" class="video-error">
            {{ videoError }}
          </div>
        </div>
      </div>

      <div class="answer-section" v-if="showAnswer">
        <strong>Answer: {{ question.answer }}</strong>
      </div>

      <div class="controls">
        <button @click="toggleAnswer" class="control-btn">
          {{ showAnswer ? 'Hide' : 'Show' }} Answer
        </button>
        <button v-if="question.type === QuestionType.YouTube"
                @click="resetVideo"
                class="control-btn video-control">
          Reset Video
        </button>
        <button @click="close" class="control-btn close-btn">Close</button>
      </div>

      <div class="award-section">
        <span class="award-label">Award +{{ question.points }}pts to:</span>
        <div class="player-btns">
          <button
              v-for="player in store.players"
              :key="player"
              @click="awardPoints(player)"
              class="player-award-btn">
            {{ player }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import type { Question } from '@/types/game'
import { QuestionType } from '@/types/game'
import YouTubePlayer from './YouTubePlayer.vue'
import { useGameStore } from '@/stores/gameStore'

interface Props {
  question: Question | null
}

interface Emits {
  (e: 'close'): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()
const store = useGameStore()

const showAnswer = ref<boolean>(false)
const audioStatus = ref<string>('Not playing...')
const videoError = ref<string>('')
const isVideoReady = ref<boolean>(false)

let audioElement: HTMLAudioElement | null = null

const playAudio = (): void => {
  if (props.question?.question) {
    audioElement = new Audio(props.question.question)
    audioElement.play()
    audioStatus.value = 'Playing...'
    audioElement.onended = () => {
      audioStatus.value = 'Not playing...'
    }
  }
}

const stopAudio = (): void => {
  if (audioElement) {
    audioElement.pause()
    audioElement.currentTime = 0
    audioStatus.value = 'Not playing...'
  }
}

const toggleAnswer = (): void => {
  showAnswer.value = !showAnswer.value
}

const resetVideo = (): void => {
  // This will re-render the YouTube component
  isVideoReady.value = false
  setTimeout(() => {
    isVideoReady.value = true
  }, 100)
}

// YouTube player event handlers
const onVideoReady = (): void => {
  isVideoReady.value = true
  videoError.value = ''
}

const onVideoPlay = (): void => {
  // Video started playing
}

const onVideoPause = (): void => {
  // Video paused
}

const onVideoEnd = (): void => {
  // Video ended
}

const onVideoError = (error: string): void => {
  videoError.value = error
}

const awardPoints = (player: string): void => {
  if (props.question) {
    store.updateScore(player, props.question.points)
  }
  close()
}

const close = (): void => {
  stopAudio()
  showAnswer.value = false
  videoError.value = ''
  isVideoReady.value = false
  emit('close')
}
</script>

<style scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0,0,0,0.8);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
  padding: 20px;
}

.modal-content {
  background: linear-gradient(to bottom, white, #ff86bd);
  padding: 20px;
  border-radius: 12px;
  max-width: 80vw;
  max-height: 85vh;
  overflow: auto;
  box-shadow: 0 10px 30px rgba(0,0,0,0.3);
}

.modal-content.video-modal {
  max-width: 90vw;
  max-height: 90vh;
  background: linear-gradient(135deg, #1a1a1a, #2d2d2d);
  color: white;
}

.question-display {
  margin-bottom: 20px;
}

.text-question {
  font-family: 'JetBrains Mono', monospace;
  font-size: 24px;
  padding: 20px;
  background: azure;
  margin-bottom: 20px;
  word-wrap: break-word;
  border-radius: 8px;
}

.image-question {
  max-width: 100%;
  max-height: 500px;
  object-fit: contain;
  border-radius: 8px;
}

.audio-controls {
  display: flex;
  align-items: center;
  gap: 10px;
  font-family: 'JetBrains Mono', monospace;
  background: azure;
  padding: 20px;
  border-radius: 8px;
}

.youtube-question {
  background: transparent;
}

.video-modal .youtube-question {
  background: transparent;
}

.video-error {
  background: #f44336;
  color: white;
  padding: 15px;
  border-radius: 8px;
  margin-top: 10px;
  text-align: center;
  font-family: 'JetBrains Mono', monospace;
}

.play-btn, .stop-btn {
  background: azure;
  border: none;
  padding: 10px 15px;
  font-size: 16px;
  cursor: pointer;
  transition: background 0.2s;
  border-radius: 5px;
}

.play-btn:hover, .stop-btn:hover {
  background: lightblue;
}

.control-btn {
  background: azure;
  border: none;
  padding: 12px 24px;
  font-family: 'JetBrains Mono', monospace;
  font-size: 16px;
  margin: 0 5px;
  cursor: pointer;
  transition: all 0.2s;
  border-radius: 6px;
}

.control-btn:hover {
  background: lightblue;
  transform: translateY(-2px);
}

.video-modal .control-btn {
  background: #4caf50;
  color: white;
}

.video-modal .control-btn:hover {
  background: #45a049;
}

.video-modal .control-btn.close-btn {
  background: #f44336;
}

.video-modal .control-btn.close-btn:hover {
  background: #d32f2f;
}

.answer-section {
  margin: 20px 0;
  padding: 20px;
  background: azure;
  font-family: 'JetBrains Mono', monospace;
  font-size: 18px;
  border-radius: 8px;
  border: 2px solid #4caf50;
}

.video-modal .answer-section {
  background: rgba(255,255,255,0.1);
  color: white;
  border-color: #4caf50;
}

.controls {
  display: flex;
  justify-content: center;
  gap: 10px;
  flex-wrap: wrap;
}

.award-section {
  margin-top: 16px;
  padding-top: 16px;
  border-top: 1px solid rgba(0,0,0,0.1);
  text-align: center;
}

.video-modal .award-section {
  border-top-color: rgba(255,255,255,0.15);
}

.award-label {
  display: block;
  font-family: 'JetBrains Mono', monospace;
  font-size: 13px;
  color: #666;
  margin-bottom: 10px;
  text-transform: uppercase;
  letter-spacing: 0.05em;
}

.video-modal .award-label {
  color: rgba(255,255,255,0.6);
}

.player-btns {
  display: flex;
  justify-content: center;
  flex-wrap: wrap;
  gap: 8px;
}

.player-award-btn {
  padding: 8px 18px;
  background: #4caf50;
  color: white;
  border: none;
  border-radius: 20px;
  font-family: 'JetBrains Mono', monospace;
  font-size: 14px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.15s;
}

.player-award-btn:hover {
  background: #388e3c;
  transform: translateY(-2px);
}

/* Responsive design */
@media (max-width: 768px) {
  .modal-content {
    max-width: 95vw;
    max-height: 90vh;
    padding: 15px;
  }

  .text-question {
    font-size: 20px;
    padding: 15px;
  }

  .control-btn {
    padding: 10px 20px;
    font-size: 14px;
    margin: 5px;
  }
}

@media (max-width: 480px) {
  .controls {
    flex-direction: column;
    align-items: center;
  }

  .control-btn {
    width: 100%;
    max-width: 200px;
  }
}
</style>
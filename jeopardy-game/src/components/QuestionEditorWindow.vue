<template>
  <div v-if="editingQuestion !== null" class="modal-overlay" @click="closeEditor">
    <div class="modal-content" @click.stop>
      <h3>Edit Question ({{ getCurrentQuestionPoints() }} points)</h3>

      <label class="form-label">Question Type:</label>
      <select v-model="questionData.type" class="input-field" @change="onTypeChange">
        <option value="text">Text</option>
        <option value="image">Image</option>
        <option value="audio">Audio</option>
        <option value="youtube">YouTube Video</option>
      </select>

      <label class="form-label">
        {{ getQuestionLabel() }}
      </label>

      <textarea v-if="questionData.type === 'text'"
                v-model="questionData.question"
                placeholder="Enter your question"
                class="input-field textarea"
                rows="4"></textarea>

      <input v-else-if="questionData.type === 'youtube'"
             v-model="questionData.question"
             placeholder="Enter YouTube URL or Video ID (e.g., https://www.youtube.com/watch?v=dQw4w9WgXcQ)"
             class="input-field youtube-input" />

      <input v-else
             v-model="questionData.question"
             :placeholder="getPlaceholder()"
             class="input-field" />

      <!-- YouTube Preview -->
      <div v-if="questionData.type === 'youtube' && questionData.question && isValidYouTubeUrl"
           class="youtube-preview">
        <label class="form-label">Preview:</label>
        <YouTubePlayer
            :video-url="questionData.question"
            width="100%"
            height="200"
            :autoplay="false"
            :controls="true"
        />
      </div>

      <div v-else-if="questionData.type === 'youtube' && questionData.question && !isValidYouTubeUrl"
           class="error-preview">
        Invalid YouTube URL or Video ID
      </div>

      <!-- Image Preview -->
      <div v-if="questionData.type === 'image' && questionData.question"
           class="image-preview">
        <label class="form-label">Preview:</label>
        <img :src="questionData.question"
             alt="Question Preview"
             class="preview-image"
             @error="onImageError"
             @load="onImageLoad" />
        <div v-if="imageError" class="error-message">
          Failed to load image
        </div>
      </div>

      <label class="form-label">Answer:</label>
      <input v-model="questionData.answer"
             placeholder="Enter the answer"
             class="input-field"
             @keyup.enter="saveQuestion" />

      <!-- YouTube Settings (Advanced) -->
      <div v-if="questionData.type === 'youtube'" class="youtube-settings">
        <details class="settings-details">
          <summary class="settings-summary">Advanced YouTube Settings</summary>
          <div class="settings-content">
            <div class="setting-row">
              <label class="setting-label">Start Time (seconds):</label>
              <input v-model.number="youtubeSettings.startTime"
                     type="number"
                     min="0"
                     class="setting-input"
                     placeholder="0" />
            </div>
            <div class="setting-row">
              <label class="setting-label">End Time (seconds):</label>
              <input v-model.number="youtubeSettings.endTime"
                     type="number"
                     min="0"
                     class="setting-input"
                     placeholder="Leave empty for full video" />
            </div>
            <div class="setting-row">
              <label class="setting-checkbox">
                <input v-model="youtubeSettings.muted" type="checkbox" />
                Start muted
              </label>
            </div>
          </div>
        </details>
      </div>

      <div class="modal-actions">
        <button @click="saveQuestion"
                :disabled="!canSave"
                class="save-btn">
          Save
        </button>
        <button @click="clearQuestion" class="clear-btn">Clear</button>
        <button @click="testQuestion"
                v-if="questionData.type !== 'text' && questionData.question"
                class="test-btn">
          Test
        </button>
        <button @click="closeEditor" class="cancel-btn">Cancel</button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, watch } from 'vue'
import type { Question, QuestionType } from '@/types/game'
import { extractYouTubeVideoId } from '@/types/game'
import YouTubePlayer from './YouTubePlayer.vue'

interface Props {
  editingQuestion: { catId: number; qId: number } | null
  categories: any[]
}

interface Emits {
  (e: 'save', data: { catId: number; qId: number; questionData: Partial<Question> }): void
  (e: 'clear', data: { catId: number; qId: number }): void
  (e: 'close'): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const questionData = reactive<Partial<Question>>({
  question: '',
  answer: '',
  type: 'text' as QuestionType
})

const youtubeSettings = reactive({
  startTime: 0,
  endTime: 0,
  muted: false
})

const imageError = ref<boolean>(false)

const isValidYouTubeUrl = computed(() => {
  if (questionData.type !== 'youtube' || !questionData.question) return false
  return extractYouTubeVideoId(questionData.question) !== null
})

const canSave = computed(() => {
  const hasQuestion = questionData.question?.trim()
  const hasAnswer = questionData.answer?.trim()

  if (questionData.type === 'youtube') {
    return hasQuestion && hasAnswer && isValidYouTubeUrl.value
  }

  return hasQuestion && hasAnswer
})

const getCurrentQuestionPoints = (): number => {
  if (!props.editingQuestion) return 0
  const { catId, qId } = props.editingQuestion
  return props.categories[catId].questions[qId].points
}

const getQuestionLabel = (): string => {
  switch (questionData.type) {
    case 'text':
      return 'Question:'
    case 'image':
      return 'Image URL:'
    case 'audio':
      return 'Audio URL:'
    case 'youtube':
      return 'YouTube URL or Video ID:'
    default:
      return 'Question:'
  }
}

const getPlaceholder = (): string => {
  switch (questionData.type) {
    case 'image':
      return 'Enter image URL (e.g., https://example.com/image.jpg)'
    case 'audio':
      return 'Enter audio URL (e.g., https://example.com/audio.mp3)'
    case 'youtube':
      return 'Enter YouTube URL or Video ID'
    default:
      return 'Enter your question'
  }
}

const onTypeChange = (): void => {
  questionData.question = ''
  imageError.value = false
  Object.assign(youtubeSettings, {
    startTime: 0,
    endTime: 0,
    muted: false
  })
}

const onImageError = (): void => {
  imageError.value = true
}

const onImageLoad = (): void => {
  imageError.value = false
}

const saveQuestion = (): void => {
  if (props.editingQuestion && canSave.value) {
    let finalQuestion = questionData.question?.trim() || ''

    // For YouTube videos, append settings if they exist
    if (questionData.type === 'youtube' && isValidYouTubeUrl.value) {
      const videoId = extractYouTubeVideoId(finalQuestion)
      if (videoId) {
        const params = new URLSearchParams()
        if (youtubeSettings.startTime > 0) {
          params.set('t', youtubeSettings.startTime.toString())
        }
        if (youtubeSettings.endTime > 0) {
          params.set('end', youtubeSettings.endTime.toString())
        }
        if (youtubeSettings.muted) {
          params.set('muted', '1')
        }

        // Store as clean YouTube URL with parameters
        finalQuestion = `https://www.youtube.com/watch?v=${videoId}`
        if (params.toString()) {
          finalQuestion += `&${params.toString()}`
        }
      }
    }

    const { catId, qId } = props.editingQuestion
    emit('save', {
      catId,
      qId,
      questionData: {
        question: finalQuestion,
        answer: questionData.answer?.trim(),
        type: questionData.type
      }
    })
  }
}

const clearQuestion = (): void => {
  if (props.editingQuestion) {
    const { catId, qId } = props.editingQuestion
    emit('clear', { catId, qId })
  }
}

const testQuestion = (): void => {
  if (!questionData.question) return

  if (questionData.type === 'image') {
    window.open(questionData.question, '_blank')
  } else if (questionData.type === 'audio') {
    const audio = new Audio(questionData.question)
    audio.play().catch(() => {
      alert('Could not play audio file')
    })
  } else if (questionData.type === 'youtube' && isValidYouTubeUrl.value) {
    const videoId = extractYouTubeVideoId(questionData.question)
    if (videoId) {
      window.open(`https://www.youtube.com/watch?v=${videoId}`, '_blank')
    }
  }
}

const closeEditor = (): void => {
  emit('close')
}

// Initialize data when editing question changes
const initializeData = (): void => {
  if (props.editingQuestion) {
    const { catId, qId } = props.editingQuestion
    const question = props.categories[catId].questions[qId]
    Object.assign(questionData, {
      question: question.question,
      answer: question.answer,
      type: question.type
    })

    // Parse YouTube settings if it's a YouTube video
    if (question.type === 'youtube' && question.question) {
      const url = new URL(question.question)
      youtubeSettings.startTime = parseInt(url.searchParams.get('t') || '0')
      youtubeSettings.endTime = parseInt(url.searchParams.get('end') || '0')
      youtubeSettings.muted = url.searchParams.get('muted') === '1'
    }
  }
}

// Watch for props changes
watch(() => props.editingQuestion, () => {
  if (props.editingQuestion) {
    initializeData()
  }
}, { immediate: true })
</script>

<style scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0,0,0,0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
  padding: 20px;
}

.modal-content {
  background: white;
  padding: 30px;
  border-radius: 12px;
  min-width: 500px;
  max-width: 800px;
  max-height: 90vh;
  overflow-y: auto;
}

.modal-content h3 {
  margin-top: 0;
  margin-bottom: 20px;
  font-size: 24px;
  text-align: center;
  color: #333;
}

.form-label {
  display: block;
  margin-bottom: 5px;
  font-weight: bold;
  color: #333;
  font-family: 'JetBrains Mono', monospace;
}

.input-field {
  width: 100%;
  padding: 12px;
  font-family: 'JetBrains Mono', monospace;
  border: 2px solid #ddd;
  border-radius: 6px;
  font-size: 14px;
  margin-bottom: 15px;
  box-sizing: border-box;
  transition: border-color 0.2s;
}

.input-field:focus {
  outline: none;
  border-color: #4caf50;
}

.textarea {
  resize: vertical;
  min-height: 100px;
  font-family: 'JetBrains Mono', monospace;
}

.youtube-input {
  font-size: 13px;
}

.youtube-preview, .image-preview {
  margin-bottom: 20px;
  padding: 15px;
  background: #f5f5f5;
  border-radius: 8px;
  border: 2px solid #e0e0e0;
}

.preview-image {
  max-width: 100%;
  max-height: 200px;
  object-fit: contain;
  border-radius: 6px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
}

.error-preview, .error-message {
  background: #f44336;
  color: white;
  padding: 10px;
  border-radius: 6px;
  text-align: center;
  font-family: 'JetBrains Mono', monospace;
  font-size: 14px;
  margin-top: 10px;
}

.youtube-settings {
  margin-bottom: 20px;
}

.settings-details {
  border: 2px solid #e0e0e0;
  border-radius: 8px;
  overflow: hidden;
}

.settings-summary {
  background: #f5f5f5;
  padding: 12px 15px;
  cursor: pointer;
  font-family: 'JetBrains Mono', monospace;
  font-weight: bold;
  user-select: none;
  transition: background 0.2s;
}

.settings-summary:hover {
  background: #e9e9e9;
}

.settings-content {
  padding: 15px;
  background: white;
}

.setting-row {
  display: flex;
  align-items: center;
  margin-bottom: 12px;
  gap: 10px;
}

.setting-label {
  min-width: 140px;
  font-family: 'JetBrains Mono', monospace;
  font-size: 13px;
  color: #666;
}

.setting-input {
  flex: 1;
  padding: 8px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-family: 'JetBrains Mono', monospace;
  font-size: 13px;
}

.setting-checkbox {
  display: flex;
  align-items: center;
  gap: 8px;
  font-family: 'JetBrains Mono', monospace;
  font-size: 13px;
  cursor: pointer;
  user-select: none;
}

.setting-checkbox input[type="checkbox"] {
  width: 16px;
  height: 16px;
}

.modal-actions {
  display: flex;
  justify-content: center;
  gap: 12px;
  margin-top: 25px;
  flex-wrap: wrap;
}

.save-btn, .clear-btn, .cancel-btn, .test-btn {
  padding: 12px 24px;
  border: none;
  cursor: pointer;
  font-family: 'JetBrains Mono', monospace;
  font-size: 14px;
  border-radius: 6px;
  transition: all 0.2s;
  min-width: 80px;
}

.save-btn {
  background: #4caf50;
  color: white;
}

.save-btn:hover:not(:disabled) {
  background: #45a049;
  transform: translateY(-1px);
}

.save-btn:disabled {
  background: #ccc;
  cursor: not-allowed;
  transform: none;
}

.clear-btn {
  background: #ff9800;
  color: white;
}

.clear-btn:hover {
  background: #f57c00;
  transform: translateY(-1px);
}

.test-btn {
  background: #2196f3;
  color: white;
}

.test-btn:hover {
  background: #1976d2;
  transform: translateY(-1px);
}

.cancel-btn {
  background: #f44336;
  color: white;
}

.cancel-btn:hover {
  background: #d32f2f;
  transform: translateY(-1px);
}

/* Responsive design */
@media (max-width: 768px) {
  .modal-content {
    min-width: auto;
    width: 95vw;
    padding: 20px;
  }

  .setting-row {
    flex-direction: column;
    align-items: flex-start;
  }

  .setting-label {
    min-width: auto;
    margin-bottom: 5px;
  }

  .setting-input {
    width: 100%;
  }

  .modal-actions {
    flex-direction: column;
    align-items: center;
  }

  .save-btn, .clear-btn, .cancel-btn, .test-btn {
    width: 100%;
    max-width: 200px;
  }
}
</style>
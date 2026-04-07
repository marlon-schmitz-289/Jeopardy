<template>
  <div class="editor">
    <div class="editor-header">
      <h1>Jeopardy Board Editor</h1>
      <div class="header-controls">
        <button @click="saveBoard" class="save-btn">Save Board</button>
        <button @click="loadBoard" class="load-btn">Load Board</button>
      </div>
    </div>

    <div class="board-editor">
      <div class="categories-row">
        <button v-for="(category, i) in categories" :key="i"
                @click="editCategory(i)"
                class="category-btn">
          {{ category.name }}
        </button>
      </div>

      <div class="questions-grid">
        <div v-for="(category, catId) in categories" :key="catId" class="category-column">
          <button v-for="(question, qId) in category.questions" :key="qId"
                  @click="editQuestion(catId, qId)"
                  class="question-btn"
                  :class="{ 
                    'has-content': question.question && question.answer,
                    'youtube-question': question.type === 'youtube',
                    'image-question': question.type === 'image',
                    'audio-question': question.type === 'audio'
                  }">
            <div class="question-points">{{ question.points }}</div>
            <div class="question-type">{{ getTypeIcon(question.type) }} {{ question.type.toUpperCase() }}</div>
          </button>
        </div>
      </div>
    </div>

    <!-- Category Editor Modal -->
    <div v-if="editingCategory !== null" class="modal-overlay" @click="closeEditor">
      <div class="modal-content" @click.stop>
        <h3>Edit Category</h3>
        <input v-model="categoryName"
               placeholder="Category name"
               class="input-field"
               @keyup.enter="saveCategory" />
        <div class="modal-actions">
          <button @click="saveCategory" class="save-btn">Save</button>
          <button @click="closeEditor" class="cancel-btn">Cancel</button>
        </div>
      </div>
    </div>

    <!-- Question Editor Modal -->
    <QuestionEditorWindow
        v-if="editingQuestion !== null"
        :editing-question="editingQuestion"
        :categories="categories"
        @save="saveQuestion"
        @clear="clearQuestion"
        @close="closeEditor"
    />
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useGameStore } from '@/stores/gameStore'
import { QuestionType } from '@/types/game'
import type { Question } from '@/types/game'
import QuestionEditorWindow from '@/components/QuestionEditorWindow.vue'

const store = useGameStore()
const { categories } = store

const editingCategory = ref<number | null>(null)
const editingQuestion = ref<{ catId: number; qId: number } | null>(null)
const categoryName = ref<string>('')

const editCategory = (index: number): void => {
  editingCategory.value = index
  categoryName.value = categories[index].name
}

const editQuestion = (catId: number, qId: number): void => {
  editingQuestion.value = { catId, qId }
}

const getTypeIcon = (type: QuestionType): string => {
  switch (type) {
    case 'text':
      return '📝'
    case 'image':
      return '🖼️'
    case 'audio':
      return '🎵'
    case 'youtube':
      return '📺'
    default:
      return '📝'
  }
}

const saveCategory = (): void => {
  if (editingCategory.value !== null && categoryName.value.trim()) {
    store.updateCategory(editingCategory.value, categoryName.value.trim())
    closeEditor()
  }
}

const saveQuestion = (data: { catId: number; qId: number; questionData: Partial<Question> }): void => {
  const { catId, qId, questionData } = data
  store.updateQuestion(catId, qId, questionData)
  closeEditor()
}

const clearQuestion = (data: { catId: number; qId: number }): void => {
  const { catId, qId } = data
  store.updateQuestion(catId, qId, {
    question: '',
    answer: '',
    type: QuestionType.Text
  })
  closeEditor()
}

const closeEditor = (): void => {
  editingCategory.value = null
  editingQuestion.value = null
}

const saveBoard = (): void => {
  const boardData = JSON.stringify(categories, null, 2)
  const blob = new Blob([boardData], { type: 'application/json' })
  const url = URL.createObjectURL(blob)
  const a = document.createElement('a')
  a.href = url
  a.download = 'jeopardy-board.json'
  a.click()
  URL.revokeObjectURL(url)
}

const loadBoard = (): void => {
  const input = document.createElement('input')
  input.type = 'file'
  input.accept = '.json'
  input.onchange = (e) => {
    const file = (e.target as HTMLInputElement).files?.[0]
    if (file) {
      const reader = new FileReader()
      reader.onload = (e) => {
        try {
          const boardData = JSON.parse(e.target?.result as string)
          if (Array.isArray(boardData) && boardData.length === 6) {
            boardData.forEach((category, index) => {
              if (categories[index]) {
                Object.assign(categories[index], category)
              }
            })
          }
        } catch (error) {
          console.error('Invalid board file:', error)
          alert('Invalid board file format')
        }
      }
      reader.readAsText(file)
    }
  }
  input.click()
}
</script>

<style scoped>
.editor {
  background: linear-gradient(to bottom, white, #ff86bd);
  height: 100%;
  overflow: hidden;
  box-sizing: border-box;
  padding: 20px;
  font-family: 'JetBrains Mono', monospace;
  display: flex;
  flex-direction: column;
}

.editor-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 30px;
  padding-left: 72px;
  -webkit-app-region: drag;
  cursor: default;
  user-select: none;
}

.editor-header h1 {
  font-size: 36px;
  margin: 0;
  -webkit-app-region: no-drag;
}

.header-controls {
  display: flex;
  gap: 10px;
  -webkit-app-region: no-drag;
}

.categories-row {
  display: grid;
  grid-template-columns: repeat(6, 1fr);
  gap: 5px;
  margin-bottom: 10px;
}

.board-editor {
  flex: 1;
  display: flex;
  flex-direction: column;
  min-height: 0;
}

.questions-grid {
  display: grid;
  grid-template-columns: repeat(6, 1fr);
  gap: 5px;
  flex: 1;
  min-height: 0;
}

.category-column {
  display: grid;
  grid-template-rows: repeat(5, 1fr);
  gap: 5px;
}

.category-btn, .question-btn {
  background: white;
  border: none;
  padding: 15px;
  font-family: 'JetBrains Mono', monospace;
  font-size: 16px;
  cursor: pointer;
  border-radius: 5px;
  transition: all 0.2s;
}

.category-btn:hover, .question-btn:hover {
  background: lightblue;
  transform: translateY(-2px);
}

.question-btn {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 5px;
}

.question-btn.has-content {
  background: #e6ffe6;
  border: 2px solid #4caf50;
}

.question-btn.youtube-question.has-content {
  background: #ffebee;
  border-color: #f44336;
}

.question-btn.image-question.has-content {
  background: #e8f5e8;
  border-color: #2196f3;
}

.question-btn.audio-question.has-content {
  background: #fff3e0;
  border-color: #ff9800;
}

.question-points {
  font-size: 18px;
  font-weight: bold;
}

.question-type {
  font-size: 10px;
  color: #666;
  text-transform: uppercase;
  text-align: center;
  line-height: 1.2;
}

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
}

.modal-content {
  background: white;
  padding: 30px;
  border-radius: 8px;
  min-width: 400px;
  max-width: 600px;
  max-height: 80vh;
  overflow-y: auto;
}

.modal-content h3 {
  margin-top: 0;
  margin-bottom: 20px;
  font-size: 24px;
  text-align: center;
}

.input-field {
  width: 100%;
  padding: 10px;
  font-family: 'JetBrains Mono', monospace;
  border: 2px solid #ddd;
  border-radius: 5px;
  font-size: 16px;
  margin-bottom: 15px;
  box-sizing: border-box;
}

.input-field:focus {
  outline: none;
  border-color: #4caf50;
}

.modal-actions {
  display: flex;
  justify-content: center;
  gap: 10px;
  margin-top: 20px;
}

.save-btn, .load-btn, .cancel-btn {
  padding: 10px 20px;
  border: none;
  cursor: pointer;
  font-family: 'JetBrains Mono', monospace;
  font-size: 16px;
  border-radius: 5px;
  transition: background 0.2s;
}

.save-btn {
  background: #4caf50;
  color: white;
}

.save-btn:hover {
  background: #45a049;
}

.load-btn {
  background: #2196f3;
  color: white;
}

.load-btn:hover {
  background: #1976d2;
}

.cancel-btn {
  background: #f44336;
  color: white;
}

.cancel-btn:hover {
  background: #d32f2f;
}
</style>
<template>
  <div class="game-board">
    <div class="categories">
      <div v-for="category in categories" :key="category.id" class="category-header">
        {{ category.name }}
      </div>
    </div>

    <div class="questions-grid">
      <div v-for="category in categories" :key="category.id" class="category-column">
        <button
            v-for="question in category.questions"
            :key="question.id"
            :disabled="question.used"
            @click="selectQuestion(category.id, question.id)"
            class="question-btn"
            :class="{ used: question.used }"
        >
          {{ question.points }}
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useGameStore } from '@/stores/gameStore'
import type { Question, QuestionSelection } from '@/types/game'

interface Emits {
  (e: 'question-selected', selection: QuestionSelection): void
}

const emit = defineEmits<Emits>()
const store = useGameStore()
const { categories } = store

const selectQuestion = (categoryId: number, questionId: number): void => {
  const question: Question = categories[categoryId].questions[questionId]
  emit('question-selected', { categoryId, questionId, question })
}
</script>

<style scoped>
.game-board {
  background: linear-gradient(to bottom, white, #ff86bd);
  padding: 20px;
  min-height: 100vh;
}

.categories {
  display: grid;
  grid-template-columns: repeat(6, 1fr);
  gap: 5px;
  margin-bottom: 10px;
}

.category-header {
  background: azure;
  color: black;
  padding: 10px;
  text-align: center;
  font-family: 'JetBrains Mono', monospace;
  font-size: 16px;
}

.questions-grid {
  display: grid;
  grid-template-columns: repeat(6, 1fr);
  gap: 5px;
}

.category-column {
  display: grid;
  grid-template-rows: repeat(5, 1fr);
  gap: 5px;
}

.question-btn {
  background: white;
  border: none;
  padding: 20px;
  font-family: 'JetBrains Mono', monospace;
  font-size: 20px;
  cursor: pointer;
  transition: background 0.2s;
}

.question-btn:hover:not(:disabled) {
  background: lightblue;
}

.question-btn:active {
  background: deepskyblue;
}

.question-btn.used {
  background: lightgray;
  cursor: not-allowed;
}
</style>
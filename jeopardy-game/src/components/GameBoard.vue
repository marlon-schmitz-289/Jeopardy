﻿<template>
  <div class="game-board"
       :style="{ '--col-count': categories.length, '--row-count': maxRows }">
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
import { computed } from 'vue'
import { useGameStore } from '@/stores/gameStore'
import type { Question, QuestionSelection } from '@/types/game'

interface Emits {
  (e: 'question-selected', selection: QuestionSelection): void
}

const emit = defineEmits<Emits>()
const store = useGameStore()
const { categories } = store

const maxRows = computed(() =>
  Math.max(...categories.map(c => c.questions.length), 1)
)

const selectQuestion = (categoryId: number, questionId: number): void => {
  const question: Question = categories[categoryId].questions[questionId]
  emit('question-selected', { categoryId, questionId, question })
}
</script>

<style scoped>
.game-board {
  height: 100%;
  width: 100%;
  box-sizing: border-box;
  display: grid;
  grid-template-rows: auto 1fr;
  gap: 6px;
  padding: 12px;
  overflow: hidden;
  background: linear-gradient(to bottom, var(--lt-bg-start, #ffffff), var(--lt-bg-end, #f0f0f0));
}

.categories {
  display: grid;
  grid-template-columns: repeat(var(--col-count, 6), 1fr);
  gap: 6px;
}

.category-header {
  background: var(--lt-card, #e2e8f0);
  color: #1e293b;
  padding: clamp(6px, 1.2vh, 14px) clamp(6px, 1vw, 16px);
  text-align: center;
  font-family: 'JetBrains Mono', monospace;
  font-size: clamp(11px, 1.1vw, 18px);
  font-weight: 700;
  border-radius: 10px;
  text-transform: uppercase;
  letter-spacing: 0.03em;
  display: flex;
  align-items: center;
  justify-content: center;
  word-break: break-word;
  hyphens: auto;
  line-height: 1.2;
}

.questions-grid {
  display: grid;
  grid-template-columns: repeat(var(--col-count, 6), 1fr);
  gap: 6px;
  min-height: 0;
}

.category-column {
  display: grid;
  grid-template-rows: repeat(var(--row-count, 5), 1fr);
  gap: 6px;
}

.question-btn {
  width: 100%;
  height: 100%;
  background: var(--lt-card, #e2e8f0);
  color: #c34d7a;
  border: 2px solid transparent;
  border-radius: 10px;
  font-family: 'JetBrains Mono', monospace;
  font-size: clamp(14px, 2vw, 32px);
  font-weight: 700;
  cursor: pointer;
  transition: background 0.15s, transform 0.1s, border-color 0.15s, box-shadow 0.15s;
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: 0 2px 6px rgba(0,0,0,0.1);
}

.question-btn:hover:not(:disabled) {
  background: var(--lt-accent, #c34d7a);
  color: white;
  border-color: var(--lt-accent, #c34d7a);
  transform: scale(1.03);
  box-shadow: 0 4px 16px rgba(195, 77, 122, 0.3);
}

.question-btn:active:not(:disabled) {
  transform: scale(0.97);
}

.question-btn.used {
  background: rgba(0, 0, 0, 0.06);
  color: rgba(0, 0, 0, 0.2);
  border-color: transparent;
  cursor: not-allowed;
  box-shadow: none;
}

/* Dark mode */
html[data-theme="dark"] .game-board {
  background: linear-gradient(to bottom, var(--dk-bg-start, #0f172a), var(--dk-bg-end, #1a0a3c));
}

html[data-theme="dark"] .category-header {
  background: var(--dk-card, #1e293b);
  color: #e2e8f0;
}

html[data-theme="dark"] .question-btn {
  background: var(--dk-card, #1e293b);
  color: #fbbf24;
  box-shadow: 0 2px 6px rgba(0,0,0,0.3);
}

html[data-theme="dark"] .question-btn:hover:not(:disabled) {
  background: #2d3f5c;
  color: #fbbf24;
  border-color: #fbbf24;
  box-shadow: 0 4px 16px rgba(251, 191, 36, 0.25);
}

html[data-theme="dark"] .question-btn.used {
  background: rgba(30, 41, 59, 0.4);
  color: rgba(100, 116, 139, 0.3);
}
</style>
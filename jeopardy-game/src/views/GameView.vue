<template>
  <div class="game-view">
    <UserSelection
        v-if="!playersSelected"
        @players-selected="startGame"
    />

    <template v-else>
      <GameNavbar
          :show-scoreboard="showScoreboard"
          @toggle-view="showScoreboard = !showScoreboard"
          @toggle-management="showPlayerManagement = !showPlayerManagement"
          @reset-game="resetGame"
      />

      <!-- Player Management Panel -->
      <div v-if="showPlayerManagement" class="player-management-overlay">
        <div class="player-management-panel">
          <div class="panel-header">
            <h3>👥 Manage Players</h3>
            <button @click="showPlayerManagement = false" class="close-panel-btn">×</button>
          </div>

          <div class="current-players">
            <h4>Current Players:</h4>
            <div class="players-list">
              <div v-for="player in store.players" :key="player" class="player-item">
                <span class="player-info">
                  <span class="player-name">{{ player }}</span>
                  <span class="player-score">{{ store.scores[player] || 0 }} pts</span>
                </span>
                <button @click="removePlayer(player)"
                        class="remove-player-btn"
                        :disabled="store.players.length <= 1">
                  ×
                </button>
              </div>
            </div>
          </div>

          <div class="add-player-section">
            <h4>Add New Player:</h4>
            <div class="add-player-controls">
              <input v-model="newPlayerName"
                     @keyup.enter="addPlayer"
                     placeholder="Enter player name"
                     class="player-input" />
              <button @click="addPlayer"
                      :disabled="!newPlayerName.trim() || store.players.length >= 6"
                      class="add-player-btn">
                + Add
              </button>
            </div>
          </div>
        </div>
      </div>

      <!-- Main Game Content -->
      <div class="game-content">
        <ScoreBoard v-if="showScoreboard" />
        <GameBoard v-else @question-selected="showQuestion" />
      </div>

      <QuestionModal
          v-if="currentQuestion"
          :question="currentQuestion.question"
          @close="closeQuestion"
      />
    </template>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useGameStore } from '@/stores/gameStore'
import type { QuestionSelection } from '@/types/game'

import UserSelection from '@/components/UserSelection.vue'
import GameBoard from '@/components/GameBoard.vue'
import QuestionModal from '@/components/QuestionModal.vue'
import ScoreBoard from '@/components/ScoreBoard.vue'
import GameNavbar from '@/components/GameNavbar.vue'

const store = useGameStore()

const playersSelected = ref<boolean>(false)
const showScoreboard = ref<boolean>(false)
const showPlayerManagement = ref<boolean>(false)
const currentQuestion = ref<QuestionSelection | null>(null)
const newPlayerName = ref<string>('')

const startGame = (players: string[]): void => {
  store.setPlayers(players)
  playersSelected.value = true
}

const showQuestion = (data: QuestionSelection): void => {
  currentQuestion.value = data
}

const closeQuestion = (): void => {
  if (currentQuestion.value) {
    store.markQuestionUsed(
        currentQuestion.value.categoryId,
        currentQuestion.value.questionId
    )
  }
  currentQuestion.value = null
}

const addPlayer = (): void => {
  const playerName = newPlayerName.value.trim()
  if (playerName && !store.players.includes(playerName) && store.players.length < 6) {
    store.addPlayer(playerName)
    newPlayerName.value = ''
  }
}

const removePlayer = (playerName: string): void => {
  if (store.players.length > 1) {
    store.removePlayer(playerName)
  }
}

const resetGame = (): void => {
  if (confirm('Are you sure you want to reset the game? All progress will be lost.')) {
    store.resetGame()
    playersSelected.value = false
    showScoreboard.value = false
    showPlayerManagement.value = false
    currentQuestion.value = null
    newPlayerName.value = ''
  }
}
</script>

<style scoped>
.game-view {
  min-height: 100vh;
  position: relative;
}

.game-content {
  padding-top: 80px; /* Space for GameNavbar */
  min-height: 100vh;
}

.player-management-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0,0,0,0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1500;
  padding: 20px;
}

.player-management-panel {
  background: linear-gradient(135deg, white, #f8f9fa);
  border-radius: 20px;
  padding: 30px;
  max-width: 500px;
  width: 100%;
  max-height: 80vh;
  overflow-y: auto;
  box-shadow: 0 12px 40px rgba(0,0,0,0.2);
  backdrop-filter: blur(12px);
}

.panel-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 25px;
  padding-bottom: 15px;
  border-bottom: 2px solid #e9ecef;
}

.panel-header h3 {
  margin: 0;
  font-family: 'JetBrains Mono', monospace;
  font-size: 24px;
  color: #333;
}

.close-panel-btn {
  background: #f44336;
  color: white;
  border: none;
  width: 36px;
  height: 36px;
  border-radius: 50%;
  font-size: 20px;
  font-weight: bold;
  cursor: pointer;
  transition: all 0.2s;
  display: flex;
  align-items: center;
  justify-content: center;
}

.close-panel-btn:hover {
  background: #d32f2f;
  transform: scale(1.1);
}

.current-players h4,
.add-player-section h4 {
  font-family: 'JetBrains Mono', monospace;
  color: #555;
  margin-bottom: 15px;
  font-size: 18px;
}

.players-list {
  display: flex;
  flex-direction: column;
  gap: 12px;
  margin-bottom: 25px;
}

.player-item {
  display: flex;
  align-items: center;
  justify-content: space-between;
  background: rgba(255,255,255,0.8);
  padding: 15px 20px;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  transition: all 0.2s;
}

.player-item:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0,0,0,0.15);
}

.player-info {
  display: flex;
  flex-direction: column;
  gap: 4px;
}

.player-name {
  font-weight: bold;
  color: #333;
  font-size: 16px;
}

.player-score {
  color: #666;
  font-size: 14px;
  font-family: 'JetBrains Mono', monospace;
}

.remove-player-btn {
  background: linear-gradient(135deg, #f44336, #d32f2f);
  color: white;
  border: none;
  width: 32px;
  height: 32px;
  border-radius: 50%;
  font-size: 16px;
  font-weight: bold;
  cursor: pointer;
  transition: all 0.2s;
  display: flex;
  align-items: center;
  justify-content: center;
}

.remove-player-btn:hover:not(:disabled) {
  background: linear-gradient(135deg, #d32f2f, #b71c1c);
  transform: scale(1.1);
}

.remove-player-btn:disabled {
  background: #ccc;
  cursor: not-allowed;
  transform: none;
}

.add-player-controls {
  display: flex;
  gap: 12px;
}

.player-input {
  flex: 1;
  padding: 12px 16px;
  border: 2px solid #ddd;
  border-radius: 10px;
  font-family: 'JetBrains Mono', monospace;
  font-size: 14px;
  transition: border-color 0.2s;
}

.player-input:focus {
  outline: none;
  border-color: #4caf50;
  box-shadow: 0 0 0 3px rgba(76, 175, 80, 0.2);
}

.add-player-btn {
  padding: 12px 20px;
  background: linear-gradient(135deg, #4caf50, #45a049);
  color: white;
  border: none;
  border-radius: 10px;
  font-family: 'JetBrains Mono', monospace;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.2s;
  white-space: nowrap;
}

.add-player-btn:hover:not(:disabled) {
  background: linear-gradient(135deg, #45a049, #388e3c);
  transform: translateY(-1px);
}

.add-player-btn:disabled {
  background: #ccc;
  cursor: not-allowed;
  transform: none;
}

/* Responsive design */
@media (max-width: 768px) {
  .game-content {
    padding-top: 70px;
  }

  .player-management-panel {
    padding: 20px;
    margin: 10px;
    border-radius: 16px;
  }

  .panel-header h3 {
    font-size: 20px;
  }

  .add-player-controls {
    flex-direction: column;
  }
}

@media (max-width: 640px) {
  .game-content {
    padding-top: 60px;
  }
}
</style>
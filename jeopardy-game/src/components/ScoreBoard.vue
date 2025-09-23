<template>
  <div class="scoreboard">
    <h1>Punktestand</h1>

    <div class="players-grid" :class="{ 'three-players': players.length === 3 }">
      <div v-for="player in players" :key="player" class="player-section">
        <div class="player-name">{{ player }}</div>
        <div class="player-score"
             @click="adjustScore(player, 200)"
             @contextmenu.prevent="adjustScore(player, -200)"
             :title="'Left click: +100, Right click: -100'">
          {{ scores[player] || 0 }}
        </div>
      </div>
    </div>

    <div class="controls">
      <button @click="resetScores" class="control-btn">Reset Scores</button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useGameStore } from '@/stores/gameStore'

const store = useGameStore()
const { players, scores } = store

const adjustScore = (player: string, points: number): void => {
  store.updateScore(player, points)
}

const resetScores = (): void => {
  Object.keys(scores).forEach(player => {
    store.updateScore(player, -scores[player])
  })
}
</script>

<style scoped>
.scoreboard {
  background: linear-gradient(to bottom, white, #ff86bd);
  padding: 40px;
  text-align: center;
  min-height: 100vh;
}

h1 {
  font-size: 60px;
  font-weight: bold;
  margin-bottom: 30px;
  font-family: 'JetBrains Mono', monospace;
}

.players-grid {
  display: grid;
  grid-template-columns: repeat(2, 280px);
  gap: 10px;
  justify-content: center;
  margin-bottom: 30px;
}

.players-grid.three-players {
  grid-template-columns: repeat(3, 280px);
}

.player-section {
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 4px 8px rgba(0,0,0,0.1);
}

.player-name {
  background: #c34d7a;
  color: white;
  padding: 15px;
  font-size: 40px;
  font-weight: bold;
  font-family: 'JetBrains Mono', monospace;
}

.player-score {
  background: #e9d0d7;
  padding: 15px;
  font-size: 40px;
  font-weight: bold;
  cursor: pointer;
  user-select: none;
  transition: background 0.2s;
  font-family: 'JetBrains Mono', monospace;
}

.player-score:hover {
  background: #d0b7c2;
}

.controls {
  display: flex;
  justify-content: center;
  gap: 10px;
}

.control-btn {
  background: azure;
  border: none;
  padding: 15px 30px;
  font-family: 'JetBrains Mono', monospace;
  font-size: 18px;
  cursor: pointer;
  border-radius: 5px;
  transition: background 0.2s;
}

.control-btn:hover {
  background: lightblue;
}
</style>
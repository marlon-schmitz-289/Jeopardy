<template>
  <div class="scoreboard">
    <Transition name="overlay">
      <div v-if="gameEnded" class="winner-overlay">
        <div class="confetti-container">
          <span
            v-for="(item, i) in confettiItems"
            :key="i"
            class="confetti-item"
            :style="{
              left: item.left + '%',
              animationDelay: item.delay + 's',
              animationDuration: item.duration + 's',
              fontSize: item.size + 'px'
            }">{{ item.emoji }}</span>
        </div>

        <!-- Podium: 2nd | 1st | 3rd -->
        <div class="podium-row">
          <div v-if="podium[1]" class="podium-entry rank-2">
            <div class="p-crown silver-crown">👑</div>
            <div class="p-place">2nd</div>
            <div v-for="p in podium[1].players" :key="p" class="p-name">{{ p }}</div>
            <div class="p-score">{{ podium[1].score }} pts</div>
            <div class="platform plat-2"></div>
          </div>

          <div v-if="podium[0]" class="podium-entry rank-1">
            <div class="p-crown gold-crown">👑</div>
            <div class="p-place gold-place">{{ podium[0].players.length > 1 ? 'TIE!' : 'WINNER!' }}</div>
            <div v-for="p in podium[0].players" :key="p" class="p-name p-name-big">{{ p }}</div>
            <div class="p-score">{{ podium[0].score }} pts</div>
            <div class="platform plat-1"></div>
          </div>

          <div v-if="podium[2]" class="podium-entry rank-3">
            <div class="p-crown copper-crown">👑</div>
            <div class="p-place">3rd</div>
            <div v-for="p in podium[2].players" :key="p" class="p-name">{{ p }}</div>
            <div class="p-score">{{ podium[2].score }} pts</div>
            <div class="platform plat-3"></div>
          </div>
        </div>

        <button @click="playAgain" class="play-again-btn">Play Again</button>
      </div>
    </Transition>

    <h1>Punktestand</h1>

    <div class="players-grid" :class="{ 'three-players': store.players.length === 3 }">
      <div
        v-for="player in store.players"
        :key="player"
        class="player-section"
        :class="{
          'rank-glow-1': gameEnded && playerRanks[player] === 1,
          'rank-glow-2': gameEnded && playerRanks[player] === 2,
          'rank-glow-3': gameEnded && playerRanks[player] === 3,
        }">
        <div class="player-name">{{ player }}</div>
        <div class="player-score"
             @click="adjustScore(player, 200)"
             @contextmenu.prevent="adjustScore(player, -200)"
             :title="'Left click: +100, Right click: -100'">
          {{ store.scores[player] || 0 }}
        </div>
      </div>
    </div>

    <div class="controls">
      <button @click="resetScores" class="control-btn">Reset Scores</button>
      <button @click="endGame" class="control-btn end-btn">End Game 🏁</button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { useGameStore } from '@/stores/gameStore'

interface Emits {
  (e: 'game-reset'): void
}

const emit = defineEmits<Emits>()
const store = useGameStore()
const gameEnded = ref(false)

interface ConfettiItem {
  emoji: string
  left: number
  delay: number
  duration: number
  size: number
}
const confettiItems = ref<ConfettiItem[]>([])

interface PodiumEntry {
  rank: number
  players: string[]
  score: number
}

const podium = computed((): PodiumEntry[] => {
  if (store.players.length === 0) return []
  const uniqueScores = [...new Set(store.players.map(p => store.scores[p] ?? 0))]
    .sort((a, b) => b - a)
  return uniqueScores.slice(0, 3).map((score, i) => ({
    rank: i + 1,
    players: store.players.filter(p => (store.scores[p] ?? 0) === score),
    score
  }))
})

const playerRanks = computed(() => {
  const ranks: Record<string, number> = {}
  podium.value.forEach(entry => {
    entry.players.forEach(p => { ranks[p] = entry.rank })
  })
  return ranks
})

const endGame = (): void => {
  const emojis = ['⭐', '🌟', '✨', '🎉', '🎊']
  confettiItems.value = Array.from({ length: 25 }, (_, i) => ({
    emoji: emojis[i % emojis.length],
    left: Math.random() * 96,
    delay: Math.random() * 2,
    duration: 2.5 + Math.random() * 2,
    size: 18 + Math.floor(Math.random() * 22)
  }))
  gameEnded.value = true
}

const playAgain = (): void => {
  store.resetGame()
  gameEnded.value = false
  emit('game-reset')
}

const adjustScore = (player: string, points: number): void => {
  store.updateScore(player, points)
}

const resetScores = (): void => {
  Object.keys(store.scores).forEach(player => {
    store.updateScore(player, -store.scores[player])
  })
}
</script>

<style scoped>
/* ── Overlay transition ── */
.overlay-enter-active { transition: opacity 0.4s ease; }
.overlay-leave-active { transition: opacity 0.3s ease; }
.overlay-enter-from,
.overlay-leave-to     { opacity: 0; }

/* ── Winner overlay ── */
.winner-overlay {
  position: fixed;
  inset: 0;
  background: linear-gradient(135deg, #0d0020 0%, #3a0050 50%, #0d0020 100%);
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: flex-end;
  padding-bottom: 48px;
  z-index: 2000;
  overflow: hidden;
}

/* ── Confetti ── */
.confetti-container {
  position: absolute;
  inset: 0;
  pointer-events: none;
}

@keyframes confettiFall {
  0%   { transform: translateY(-60px) rotate(0deg);   opacity: 1; }
  100% { transform: translateY(110vh) rotate(720deg); opacity: 0.6; }
}

.confetti-item {
  position: absolute;
  top: 0;
  animation: confettiFall linear infinite;
}

/* ── Podium row ── */
.podium-row {
  display: flex;
  align-items: flex-end;
  justify-content: center;
  gap: 8px;
  margin-bottom: 32px;
}

.podium-entry {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 4px;
}

/* ── Platforms (the physical steps) ── */
.platform {
  width: 100%;
  border-radius: 4px 4px 0 0;
  margin-top: 12px;
}
.plat-1 { height: 80px; background: linear-gradient(180deg, #b8860b, #8b6914); min-width: 180px; }
.plat-2 { height: 55px; background: linear-gradient(180deg, #9e9e9e, #757575); min-width: 150px; }
.plat-3 { height: 35px; background: linear-gradient(180deg, #8d5524, #6d3b17); min-width: 150px; }

/* ── Crown animations ── */
@keyframes crownDrop {
  0%   { transform: translateY(-350px) rotate(-25deg) scale(0.4); opacity: 0; }
  60%  { transform: translateY(18px)   rotate(8deg)  scale(1.25); opacity: 1; }
  75%  { transform: translateY(-12px)  rotate(-4deg) scale(1.1); }
  88%  { transform: translateY(6px)    rotate(2deg)  scale(1.05); }
  100% { transform: translateY(0)      rotate(0deg)  scale(1); }
}

@keyframes crownSlideLeft {
  0%   { transform: translateX(-280px) rotate(-20deg) scale(0.4); opacity: 0; }
  65%  { transform: translateX(12px)   rotate(5deg)  scale(1.15); opacity: 1; }
  80%  { transform: translateX(-6px)   rotate(-2deg) scale(1.05); }
  100% { transform: translateX(0)      rotate(0deg)  scale(1); }
}

@keyframes crownSlideRight {
  0%   { transform: translateX(280px)  rotate(20deg)  scale(0.4); opacity: 0; }
  65%  { transform: translateX(-12px)  rotate(-5deg)  scale(1.15); opacity: 1; }
  80%  { transform: translateX(6px)    rotate(2deg)   scale(1.05); }
  100% { transform: translateX(0)      rotate(0deg)   scale(1); }
}

.p-crown {
  line-height: 1;
  animation-duration: 0.85s;
  animation-timing-function: cubic-bezier(0.34, 1.56, 0.64, 1);
  animation-fill-mode: both;
}

/* 2nd — silver, slides from left first */
.silver-crown {
  font-size: 64px;
  filter: grayscale(1) brightness(1.6) drop-shadow(0 4px 14px rgba(192,192,192,0.9));
  animation-name: crownSlideLeft;
  animation-delay: 0s;
}

/* 1st — gold, drops last for drama */
.gold-crown {
  font-size: 92px;
  filter: drop-shadow(0 4px 18px rgba(255,215,0,0.95));
  animation-name: crownDrop;
  animation-delay: 0.9s;
}

/* 3rd — copper, slides from right */
.copper-crown {
  font-size: 60px;
  filter: hue-rotate(-25deg) saturate(2) brightness(0.78) drop-shadow(0 4px 14px rgba(184,115,51,0.9));
  animation-name: crownSlideRight;
  animation-delay: 0.4s;
}

/* ── Podium text ── */
@keyframes popIn {
  0%   { transform: scale(0); opacity: 0; }
  70%  { transform: scale(1.12); }
  100% { transform: scale(1);   opacity: 1; }
}

.p-place {
  font-family: 'JetBrains Mono', monospace;
  font-weight: bold;
  color: rgba(255,255,255,0.75);
  animation: popIn 0.4s both;
}

.rank-2 .p-place { font-size: 22px; animation-delay: 0.55s; }
.rank-1 .p-place { font-size: 36px; animation-delay: 1.5s; }
.rank-3 .p-place { font-size: 22px; animation-delay: 0.95s; }

.gold-place { color: gold; text-shadow: 0 0 30px rgba(255,215,0,0.9); }

.p-name {
  font-family: 'JetBrains Mono', monospace;
  font-weight: bold;
  color: white;
  text-shadow: 0 2px 8px rgba(0,0,0,0.5);
  animation: popIn 0.4s both;
}

.rank-2 .p-name { font-size: 28px; animation-delay: 0.7s; }
.rank-1 .p-name { font-size: 44px; animation-delay: 1.65s; }
.rank-3 .p-name { font-size: 26px; animation-delay: 1.1s; }

.p-name-big { font-size: 48px !important; }

.p-score {
  font-family: 'JetBrains Mono', monospace;
  color: rgba(255,255,255,0.55);
  animation: popIn 0.4s both;
}

.rank-2 .p-score { font-size: 16px; animation-delay: 0.85s; }
.rank-1 .p-score { font-size: 22px; animation-delay: 1.8s; }
.rank-3 .p-score { font-size: 16px; animation-delay: 1.25s; }

/* ── Play Again ── */
.play-again-btn {
  padding: 14px 44px;
  background: linear-gradient(135deg, #4caf50, #2e7d32);
  color: white;
  border: none;
  border-radius: 32px;
  font-family: 'JetBrains Mono', monospace;
  font-size: 22px;
  font-weight: bold;
  cursor: pointer;
  transition: transform 0.15s, background 0.2s;
  animation: popIn 0.45s 2.1s both;
  position: relative;
  z-index: 1;
}

.play-again-btn:hover {
  background: linear-gradient(135deg, #388e3c, #1b5e20);
  transform: scale(1.06);
}

/* ── Scoreboard ── */
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

@keyframes goldGlow   { 0%,100% { box-shadow: 0 0 14px 3px rgba(255,215,0,0.5);  } 50% { box-shadow: 0 0 32px 7px rgba(255,215,0,0.95);  } }
@keyframes silverGlow { 0%,100% { box-shadow: 0 0 14px 3px rgba(192,192,192,0.5); } 50% { box-shadow: 0 0 32px 7px rgba(192,192,192,0.95); } }
@keyframes copperGlow { 0%,100% { box-shadow: 0 0 14px 3px rgba(184,115,51,0.5); } 50% { box-shadow: 0 0 32px 7px rgba(184,115,51,0.95); } }

.rank-glow-1 { border: 3px solid gold;              animation: goldGlow   1.2s ease-in-out infinite; }
.rank-glow-2 { border: 3px solid silver;            animation: silverGlow 1.2s ease-in-out infinite; }
.rank-glow-3 { border: 3px solid #b87333;           animation: copperGlow 1.2s ease-in-out infinite; }

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

.player-score:hover { background: #d0b7c2; }

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

.control-btn:hover { background: lightblue; }

.end-btn {
  background: linear-gradient(135deg, #e91e63, #c2185b);
  color: white;
}

.end-btn:hover {
  background: linear-gradient(135deg, #c2185b, #ad1457);
}
</style>

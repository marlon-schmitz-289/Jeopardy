﻿<template>
  <nav class="game-navbar" :class="{ 'navbar-visible': isVisible }">
    <div class="navbar-content">
      <div class="navbar-brand">
        <span class="game-title">🎯 Jeopardy</span>
      </div>

      <div class="navbar-controls">
        <button @click="$emit('toggle-view')" class="nav-btn toggle-btn">
          <span class="btn-icon">{{ showScoreboard ? '🎯' : '🏆' }}</span>
          <span class="btn-text">{{ showScoreboard ? 'Show Board' : 'Show Scores' }}</span>
        </button>

        <button @click="$emit('toggle-management')" class="nav-btn manage-btn">
          <span class="btn-icon">👥</span>
          <span class="btn-text">Players</span>
        </button>

        <button @click="$emit('reset-game')" class="nav-btn reset-btn">
          <span class="btn-icon">🔄</span>
          <span class="btn-text">Reset</span>
        </button>

        <button @click="toggleTheme" class="nav-btn theme-btn">
          <span class="btn-icon">{{ isDark ? '☀️' : '🌙' }}</span>
        </button>
      </div>

      <div class="navbar-toggle" @click="toggleNavbar">
        <span class="hamburger-line"></span>
        <span class="hamburger-line"></span>
        <span class="hamburger-line"></span>
      </div>
    </div>
  </nav>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useTheme } from '@/composables/useTheme'

interface Props {
  showScoreboard: boolean
}

interface Emits {
  (e: 'toggle-view'): void
  (e: 'toggle-management'): void
  (e: 'reset-game'): void
}

defineProps<Props>()
defineEmits<Emits>()

const isVisible = ref<boolean>(true)
const { isDark, toggleTheme } = useTheme()

const toggleNavbar = (): void => {
  isVisible.value = !isVisible.value
}
</script>

<style scoped>
.game-navbar {
  position: relative;
  background: linear-gradient(135deg, rgba(255,255,255,0.95), rgba(240,240,240,0.95));
  backdrop-filter: blur(12px);
  border-bottom: 2px solid rgba(255,255,255,0.2);
  box-shadow: 0 4px 20px rgba(0,0,0,0.15);
  z-index: 1000;
  transition: transform 0.3s ease;
  -webkit-app-region: drag;
  cursor: default;
  user-select: none;
  flex-shrink: 0;
}

.game-navbar.navbar-visible {
  transform: translateY(0);
}

.navbar-content {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 12px 24px 12px 88px;
  max-width: 1200px;
  margin: 0 auto;
}

.navbar-brand {
  display: flex;
  align-items: center;
  gap: 12px;
}

.game-title {
  font-family: 'JetBrains Mono', monospace;
  font-size: 20px;
  font-weight: bold;
  color: #333;
  text-shadow: 1px 1px 2px rgba(0,0,0,0.1);
}

.navbar-controls {
  display: flex;
  align-items: center;
  gap: 8px;
}

.nav-btn {
  -webkit-app-region: no-drag;
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 10px 16px;
  border: none;
  border-radius: 12px;
  font-family: 'JetBrains Mono', monospace;
  font-size: 14px;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.3s ease;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  backdrop-filter: blur(8px);
}

.btn-icon {
  font-size: 16px;
}

.btn-text {
  white-space: nowrap;
}

.toggle-btn {
  background: linear-gradient(135deg, #4fc3f7, #29b6f6);
  color: white;
}

.toggle-btn:hover {
  background: linear-gradient(135deg, #29b6f6, #1976d2);
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(41, 182, 246, 0.4);
}

.manage-btn {
  background: linear-gradient(135deg, #4caf50, #45a049);
  color: white;
}

.manage-btn:hover {
  background: linear-gradient(135deg, #45a049, #388e3c);
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(76, 175, 80, 0.4);
}

.reset-btn {
  background: linear-gradient(135deg, #f44336, #d32f2f);
  color: white;
}

.reset-btn:hover {
  background: linear-gradient(135deg, #d32f2f, #b71c1c);
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(244, 67, 54, 0.4);
}

.theme-btn {
  background: rgba(255, 255, 255, 0.15);
  color: #333;
}

.theme-btn:hover {
  background: rgba(255, 255, 255, 0.3);
  transform: translateY(-2px);
}

.navbar-toggle {
  display: none;
  flex-direction: column;
  cursor: pointer;
  padding: 8px;
  gap: 4px;
  border-radius: 8px;
  transition: background 0.2s;
}

.navbar-toggle:hover {
  background: rgba(0,0,0,0.1);
}

.hamburger-line {
  width: 24px;
  height: 3px;
  background: #333;
  border-radius: 2px;
  transition: all 0.3s ease;
}

/* Mobile Responsive */
@media (max-width: 768px) {
  .navbar-content {
    padding: 10px 16px;
  }

  .game-title {
    font-size: 18px;
  }

  .navbar-controls {
    gap: 6px;
  }

  .nav-btn {
    padding: 8px 12px;
    font-size: 13px;
  }

  .btn-text {
    display: none;
  }

  .btn-icon {
    font-size: 18px;
  }
}

@media (max-width: 640px) {
  .navbar-controls {
    position: absolute;
    top: 100%;
    left: 0;
    right: 0;
    background: linear-gradient(135deg, rgba(255,255,255,0.98), rgba(240,240,240,0.98));
    backdrop-filter: blur(12px);
    flex-direction: column;
    padding: 16px;
    gap: 12px;
    border-bottom: 2px solid rgba(255,255,255,0.2);
    box-shadow: 0 4px 20px rgba(0,0,0,0.15);
    transform: translateY(-100%);
    opacity: 0;
    visibility: hidden;
    transition: all 0.3s ease;
  }

  .navbar-visible .navbar-controls {
    transform: translateY(0);
    opacity: 1;
    visibility: visible;
  }

  .navbar-toggle {
    display: flex;
  }

  .nav-btn {
    width: 100%;
    justify-content: center;
    padding: 12px 20px;
    font-size: 16px;
  }

  .btn-text {
    display: block;
  }
}
</style>
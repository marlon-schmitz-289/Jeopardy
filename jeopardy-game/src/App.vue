<template>
  <div id="app">
    <nav class="main-nav" v-if="showNavigation">
      <router-link to="/game" class="nav-link">🎮 Game</router-link>
      <router-link to="/editor" class="nav-link">✏️ Editor</router-link>
    </nav>

    <main class="main-content" :class="{ 'no-nav': !showNavigation }">
      <router-view />
    </main>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()

// Hide main navigation completely when in GameView (regardless of playing state)
const showNavigation = computed((): boolean => {
  return route.name !== 'Game'
})
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=JetBrains+Mono:wght@400;700&display=swap');

* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  font-family: 'JetBrains Mono', monospace;
  line-height: 1.6;
  color: #333;
  overflow-x: hidden;
}

#app {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.main-nav {
  background: linear-gradient(135deg, #333, #444);
  padding: 16px 24px;
  display: flex;
  gap: 24px;
  box-shadow: 0 4px 12px rgba(0,0,0,0.15);
  backdrop-filter: blur(8px);
}

.nav-link {
  color: white;
  text-decoration: none;
  padding: 12px 24px;
  border-radius: 12px;
  transition: all 0.3s ease;
  font-weight: 500;
  font-size: 16px;
  display: flex;
  align-items: center;
  gap: 8px;
}

.nav-link:hover {
  background: rgba(255,255,255,0.15);
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(255,255,255,0.1);
}

.nav-link.router-link-active {
  background: linear-gradient(135deg, #4caf50, #45a049);
  box-shadow: 0 4px 12px rgba(76, 175, 80, 0.3);
}

.main-content {
  flex: 1;
  position: relative;
}

.main-content.no-nav {
  /* Full height when no main nav is shown */
  min-height: 100vh;
}

/* Utility classes */
.text-center {
  text-align: center;
}

.mt-4 {
  margin-top: 1rem;
}

.mb-4 {
  margin-bottom: 1rem;
}

.p-4 {
  padding: 1rem;
}

/* Responsive design */
@media (max-width: 768px) {
  .main-nav {
    flex-direction: column;
    gap: 12px;
    padding: 12px 20px;
  }

  .nav-link {
    text-align: center;
    justify-content: center;
    padding: 12px 20px;
  }
}

@media (max-width: 480px) {
  .main-nav {
    gap: 8px;
    padding: 10px 16px;
  }

  .nav-link {
    padding: 10px 16px;
    font-size: 14px;
  }
}
</style>
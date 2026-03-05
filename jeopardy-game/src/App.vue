<template>
  <div id="app">
    <nav class="main-nav">
      <router-link to="/game" class="nav-link">Game</router-link>
      <router-link to="/editor" class="nav-link">Editor</router-link>
      <router-link to="/settings" class="nav-link">Settings</router-link>
    </nav>

    <main class="main-content">
      <router-view />
    </main>
  </div>
</template>

<script setup lang="ts">
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=JetBrains+Mono:wght@400;700&display=swap');

* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  scrollbar-width: thin;
  scrollbar-color: var(--lt-accent) transparent;
}

::-webkit-scrollbar {
  width: 6px;
  height: 6px;
}

::-webkit-scrollbar-track {
  background: transparent;
}

::-webkit-scrollbar-thumb {
  background: var(--lt-accent);
  border-radius: 3px;
}

::-webkit-scrollbar-thumb:hover {
  background: #a3385e;
}

body {
  font-family: 'JetBrains Mono', monospace;
  line-height: 1.6;
  color: #333;
  overflow-x: hidden;
}

#app {
  height: 100vh;
  display: flex;
  flex-direction: column;
  overflow: hidden;
}

.main-nav {
  background: linear-gradient(135deg, #333, #444);
  padding: 16px 24px 16px 88px;
  display: flex;
  gap: 24px;
  box-shadow: 0 4px 12px rgba(0,0,0,0.15);
  backdrop-filter: blur(8px);
  -webkit-app-region: drag;
  cursor: default;
  user-select: none;
}

.nav-link {
  -webkit-app-region: no-drag;
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
  overflow: hidden;
  min-height: 0;
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

/* ============================================
   DARK MODE OVERRIDES
   ============================================ */
html[data-theme="dark"] body {
  background: var(--dk-bg-start);
  color: #f1f5f9;
}

html[data-theme="dark"] .main-nav {
  background: linear-gradient(135deg, var(--dk-card), var(--dk-bg-start));
}

html[data-theme="dark"] .nav-link {
  color: #e2e8f0;
}

html[data-theme="dark"] .nav-link.router-link-active {
  background: linear-gradient(135deg, var(--dk-accent), #6d28d9);
  box-shadow: 0 4px 12px rgba(124, 58, 237, 0.4);
}

/* User Selection */
html[data-theme="dark"] .user-selection {
  background: linear-gradient(to bottom, var(--dk-bg-start), var(--dk-bg-end));
}

html[data-theme="dark"] .selection-header h2 {
  color: #f1f5f9;
}

html[data-theme="dark"] .user-item:not(.selected) {
  background: rgba(30, 41, 59, 0.9);
  border-color: #334155;
}

html[data-theme="dark"] .user-item:not(.selected) .user-name {
  color: #f1f5f9;
}

html[data-theme="dark"] .user-input {
  background: var(--dk-card);
  border-color: #475569;
  color: #f1f5f9;
}

html[data-theme="dark"] .user-input::placeholder {
  color: #64748b;
}

html[data-theme="dark"] .user-input:focus {
  border-color: var(--dk-accent);
  box-shadow: 0 0 0 3px rgba(124, 58, 237, 0.2);
}

html[data-theme="dark"] .selected-count {
  color: #94a3b8;
}

html[data-theme="dark"] .selected-players {
  background: rgba(30, 41, 59, 0.95);
}

html[data-theme="dark"] .selected-title {
  color: #e2e8f0;
}

html[data-theme="dark"] .selected-list {
  color: #94a3b8;
}

html[data-theme="dark"] .nav-hint {
  background: rgba(30, 41, 59, 0.8);
  color: #64748b;
  border-color: rgba(51, 65, 85, 0.5);
}


/* ScoreBoard */
html[data-theme="dark"] .scoreboard {
  background: linear-gradient(to bottom, var(--dk-bg-start), var(--dk-bg-end));
  color: #f1f5f9;
}

html[data-theme="dark"] .scoreboard h1 {
  color: #f1f5f9;
}

html[data-theme="dark"] .scoreboard .player-name {
  background: var(--dk-accent);
}

html[data-theme="dark"] .scoreboard .player-score {
  background: var(--dk-card);
  color: #f1f5f9;
}

html[data-theme="dark"] .scoreboard .player-score:hover {
  background: #334155;
}

html[data-theme="dark"] .scoreboard .control-btn {
  background: var(--dk-card);
  color: #f1f5f9;
}

html[data-theme="dark"] .scoreboard .control-btn:hover {
  background: #334155;
}

/* Question Modal */
html[data-theme="dark"] .modal-content:not(.video-modal) {
  background: linear-gradient(to bottom, var(--dk-card), #2d1b69);
  color: #f1f5f9;
}

html[data-theme="dark"] .text-question {
  background: var(--dk-bg-start);
  color: #f1f5f9;
}

html[data-theme="dark"] .audio-controls {
  background: var(--dk-bg-start);
  color: #f1f5f9;
}

html[data-theme="dark"] .play-btn,
html[data-theme="dark"] .stop-btn {
  background: var(--dk-card);
  color: #f1f5f9;
}

html[data-theme="dark"] .play-btn:hover,
html[data-theme="dark"] .stop-btn:hover {
  background: #334155;
}

html[data-theme="dark"] .modal-content:not(.video-modal) .answer-section {
  background: var(--dk-bg-start);
  color: #f1f5f9;
}

html[data-theme="dark"] .modal-content:not(.video-modal) .control-btn {
  background: var(--dk-card);
  color: #f1f5f9;
}

html[data-theme="dark"] .modal-content:not(.video-modal) .control-btn:hover {
  background: var(--dk-accent);
}

html[data-theme="dark"] .award-section {
  border-top-color: rgba(255, 255, 255, 0.1);
}

html[data-theme="dark"] .award-label {
  color: #94a3b8;
}

/* Editor */
html[data-theme="dark"] .editor {
  background: linear-gradient(to bottom, var(--dk-bg-start), var(--dk-bg-end));
}

html[data-theme="dark"] .editor-header h1 {
  color: #f1f5f9;
}

html[data-theme="dark"] .editor .category-btn,
html[data-theme="dark"] .editor .question-btn {
  background: var(--dk-card);
  color: #f1f5f9;
}

html[data-theme="dark"] .editor .category-btn:hover,
html[data-theme="dark"] .editor .question-btn:hover {
  background: #334155;
}

html[data-theme="dark"] .editor .question-btn.has-content {
  background: #0d2b1e;
  border-color: #4caf50;
}

html[data-theme="dark"] .editor .question-btn.youtube-question.has-content {
  background: #2b0d12;
  border-color: #f44336;
}

html[data-theme="dark"] .editor .question-btn.image-question.has-content {
  background: #0d1e2b;
  border-color: #2196f3;
}

html[data-theme="dark"] .editor .question-btn.audio-question.has-content {
  background: #2b1e0d;
  border-color: #ff9800;
}

html[data-theme="dark"] .question-type {
  color: #94a3b8;
}

/* QuestionEditorWindow & EditorView category modal */
html[data-theme="dark"] .modal-content {
  background: var(--dk-card);
  color: #f1f5f9;
}

html[data-theme="dark"] .modal-content h3 {
  color: #f1f5f9;
}

html[data-theme="dark"] .form-label {
  color: #e2e8f0;
}

html[data-theme="dark"] .input-field {
  background: var(--dk-bg-start);
  border-color: #475569;
  color: #f1f5f9;
}

html[data-theme="dark"] .input-field:focus {
  border-color: var(--dk-accent);
}

html[data-theme="dark"] .youtube-preview,
html[data-theme="dark"] .image-preview {
  background: var(--dk-bg-start);
  border-color: #334155;
}

html[data-theme="dark"] .settings-content {
  background: var(--dk-bg-start);
}

html[data-theme="dark"] .settings-summary {
  background: var(--dk-card);
  color: #e2e8f0;
}

html[data-theme="dark"] .setting-label {
  color: #94a3b8;
}

html[data-theme="dark"] .setting-input {
  background: var(--dk-card);
  border-color: #475569;
  color: #f1f5f9;
}

/* Game Navbar */
html[data-theme="dark"] .game-navbar {
  background: linear-gradient(135deg, rgba(15, 23, 42, 0.95), rgba(30, 41, 59, 0.95));
  border-bottom-color: rgba(51, 65, 85, 0.5);
}

html[data-theme="dark"] .game-title {
  color: #e2e8f0;
}

html[data-theme="dark"] .hamburger-line {
  background: #e2e8f0;
}

/* Player Management Panel (GameView) */
html[data-theme="dark"] .player-management-panel {
  background: linear-gradient(135deg, var(--dk-card), var(--dk-bg-start));
}

html[data-theme="dark"] .panel-header {
  border-bottom-color: #334155;
}

html[data-theme="dark"] .panel-header h3 {
  color: #f1f5f9;
}

html[data-theme="dark"] .current-players h4,
html[data-theme="dark"] .add-player-section h4 {
  color: #94a3b8;
}

html[data-theme="dark"] .player-item {
  background: rgba(15, 23, 42, 0.8);
}

html[data-theme="dark"] .player-item .player-name {
  color: #f1f5f9;
  background: transparent;
}

html[data-theme="dark"] .player-item .player-score {
  color: #94a3b8;
  background: transparent;
}

html[data-theme="dark"] .player-input {
  background: var(--dk-bg-start);
  border-color: #475569;
  color: #f1f5f9;
}

html[data-theme="dark"] .player-input:focus {
  border-color: var(--dk-accent);
  box-shadow: 0 0 0 3px rgba(124, 58, 237, 0.2);
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

/* ============================================
   LIGHT MODE OVERRIDES (using CSS vars)
   ============================================ */
html[data-theme="light"] .user-selection,
html[data-theme="light"] .scoreboard,
html[data-theme="light"] .editor,
html[data-theme="light"] .modal-content:not(.video-modal) {
  background: linear-gradient(to bottom, var(--lt-bg-start), var(--lt-bg-end));
}

html[data-theme="light"] .scoreboard .player-name {
  background: var(--lt-accent);
}

html[data-theme="light"] .category-header,
html[data-theme="light"] .editor .category-btn,
html[data-theme="light"] .editor .question-btn,
html[data-theme="light"] .text-question,
html[data-theme="light"] .audio-controls,
html[data-theme="light"] .answer-section {
  background: var(--lt-card);
}

html[data-theme="light"] .nav-link.router-link-active {
  background: linear-gradient(135deg, var(--lt-accent), #a3385e);
  box-shadow: 0 4px 12px rgba(195, 77, 122, 0.3);
}

html[data-theme="light"] * {
  scrollbar-color: var(--lt-accent) transparent;
}

html[data-theme="light"] ::-webkit-scrollbar-thumb {
  background: var(--lt-accent);
}
</style>
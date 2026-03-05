<template>
  <div class="settings">
    <div class="settings-container">
      <h1 class="settings-title">⚙️ Settings</h1>

      <!-- Theme Mode -->
      <section class="settings-section">
        <h2 class="section-title">THEME MODE</h2>
        <div class="mode-buttons">
          <button
            class="mode-btn"
            :class="{ active: themeMode === 'light' }"
            @click="selectMode('light')"
          >☀️ Light</button>
          <button
            class="mode-btn"
            :class="{ active: themeMode === 'dark' }"
            @click="selectMode('dark')"
          >🌙 Dark</button>
          <button
            class="mode-btn"
            :class="{ active: themeMode === 'system' }"
            @click="selectMode('system')"
          >💻 System</button>
        </div>
      </section>

      <!-- Color Customization -->
      <section class="settings-section">
        <h2 class="section-title">COLOR CUSTOMIZATION</h2>
        <div class="color-columns">

          <!-- Light Theme -->
          <div class="color-card">
            <h3 class="card-title">Light Theme</h3>
            <div
              class="gradient-preview"
              :style="{ background: `linear-gradient(to bottom, ${store.lightColors.bgStart}, ${store.lightColors.bgEnd})` }"
            ></div>
            <div class="color-row" v-for="(label, key) in colorLabels" :key="`light-${key}`">
              <label class="color-label">{{ label }}</label>
              <div class="color-input-group">
                <input
                  type="color"
                  :value="store.lightColors[key]"
                  @input="e => store.updateLightColor(key, (e.target as HTMLInputElement).value)"
                  class="color-picker"
                />
                <span class="color-hex">{{ store.lightColors[key] }}</span>
              </div>
            </div>
            <button class="reset-btn" @click="store.resetLight()">Reset ↺</button>
          </div>

          <!-- Dark Theme -->
          <div class="color-card">
            <h3 class="card-title">Dark Theme</h3>
            <div
              class="gradient-preview"
              :style="{ background: `linear-gradient(to bottom, ${store.darkColors.bgStart}, ${store.darkColors.bgEnd})` }"
            ></div>
            <div class="color-row" v-for="(label, key) in colorLabels" :key="`dark-${key}`">
              <label class="color-label">{{ label }}</label>
              <div class="color-input-group">
                <input
                  type="color"
                  :value="store.darkColors[key]"
                  @input="e => store.updateDarkColor(key, (e.target as HTMLInputElement).value)"
                  class="color-picker"
                />
                <span class="color-hex">{{ store.darkColors[key] }}</span>
              </div>
            </div>
            <button class="reset-btn" @click="store.resetDark()">Reset ↺</button>
          </div>

        </div>
      </section>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useSettingsStore } from '@/stores/settingsStore'
import { useTheme } from '@/composables/useTheme'
import type { ColorSet } from '@/stores/settingsStore'

const store = useSettingsStore()
const { themeMode, setThemeMode } = useTheme()

const colorLabels: Record<keyof ColorSet, string> = {
  accent:  'Accent',
  bgStart: 'Bg Start',
  bgEnd:   'Bg End',
  card:    'Card',
}

function selectMode(mode: 'light' | 'dark' | 'system') {
  store.setThemeMode(mode)
  setThemeMode(mode)
}
</script>

<style scoped>
.settings {
  height: 100%;
  overflow-y: auto;
  background: linear-gradient(to bottom, white, #ff86bd);
  padding: 32px 24px;
}

.settings-container {
  max-width: 900px;
  margin: 0 auto;
}

.settings-title {
  font-size: 28px;
  font-weight: bold;
  margin-bottom: 32px;
  color: #333;
}

.settings-section {
  margin-bottom: 40px;
}

.section-title {
  font-size: 13px;
  font-weight: bold;
  letter-spacing: 0.1em;
  color: #888;
  margin-bottom: 16px;
  text-transform: uppercase;
}

/* Mode buttons */
.mode-buttons {
  display: flex;
  gap: 12px;
  flex-wrap: wrap;
}

.mode-btn {
  padding: 12px 24px;
  border: 2px solid #ddd;
  border-radius: 12px;
  background: white;
  color: #333;
  font-family: 'JetBrains Mono', monospace;
  font-size: 15px;
  cursor: pointer;
  transition: all 0.2s ease;
}

.mode-btn:hover {
  border-color: #c34d7a;
  background: rgba(195, 77, 122, 0.05);
}

.mode-btn.active {
  border-color: #c34d7a;
  background: #c34d7a;
  color: white;
  box-shadow: 0 4px 12px rgba(195, 77, 122, 0.3);
}

/* Color columns */
.color-columns {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 24px;
}

@media (max-width: 640px) {
  .color-columns {
    grid-template-columns: 1fr;
  }
}

.color-card {
  background: #f8f8f8;
  border-radius: 16px;
  padding: 20px;
  border: 1px solid #eee;
}

.card-title {
  font-size: 16px;
  font-weight: bold;
  color: #333;
  margin-bottom: 14px;
}

.gradient-preview {
  height: 60px;
  border-radius: 10px;
  margin-bottom: 16px;
  border: 1px solid rgba(0,0,0,0.08);
}

.color-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 10px;
}

.color-label {
  font-size: 13px;
  color: #555;
  min-width: 64px;
}

.color-input-group {
  display: flex;
  align-items: center;
  gap: 8px;
}

.color-picker {
  width: 36px;
  height: 36px;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  padding: 2px;
  background: transparent;
}

.color-hex {
  font-size: 12px;
  color: #666;
  font-family: 'JetBrains Mono', monospace;
  min-width: 64px;
}

.reset-btn {
  margin-top: 12px;
  padding: 8px 16px;
  border: 1px solid #ddd;
  border-radius: 8px;
  background: white;
  color: #666;
  font-family: 'JetBrains Mono', monospace;
  font-size: 13px;
  cursor: pointer;
  transition: all 0.2s ease;
  float: right;
}

.reset-btn:hover {
  border-color: #c34d7a;
  color: #c34d7a;
}

/* Dark mode overrides */
html[data-theme="dark"] .settings {
  background: linear-gradient(to bottom, var(--dk-bg-start), var(--dk-bg-end));
}

html[data-theme="dark"] .settings-title {
  color: #f1f5f9;
}

html[data-theme="dark"] .section-title {
  color: #64748b;
}

html[data-theme="dark"] .mode-btn {
  background: var(--dk-card);
  border-color: #475569;
  color: #e2e8f0;
}

html[data-theme="dark"] .mode-btn:hover {
  border-color: var(--dk-accent);
  background: rgba(124, 58, 237, 0.1);
}

html[data-theme="dark"] .mode-btn.active {
  border-color: var(--dk-accent);
  background: var(--dk-accent);
  color: white;
  box-shadow: 0 4px 12px rgba(124, 58, 237, 0.3);
}

html[data-theme="dark"] .color-card {
  background: var(--dk-card);
  border-color: #334155;
}

html[data-theme="dark"] .card-title {
  color: #f1f5f9;
}

html[data-theme="dark"] .color-label {
  color: #94a3b8;
}

html[data-theme="dark"] .color-hex {
  color: #94a3b8;
}

html[data-theme="dark"] .reset-btn {
  background: var(--dk-bg-start);
  border-color: #475569;
  color: #94a3b8;
}

html[data-theme="dark"] .reset-btn:hover {
  border-color: var(--dk-accent);
  color: var(--dk-accent);
}
</style>

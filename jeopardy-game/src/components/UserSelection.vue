<template>
  <div class="user-selection">
    <!-- Navigation Header -->
    <div class="selection-header">
      <h2>Select Players</h2>
      <router-link to="/editor" class="editor-link">
        ✏️ Board Editor
      </router-link>
    </div>

    <div class="user-list">
      <div v-for="(user, index) in users" :key="index"
           class="user-item"
           :class="{ selected: selectedUsers.includes(user) }">
        <div class="user-content" @click="toggleUser(user)">
          <span class="user-name">{{ user }}</span>
          <span v-if="selectedUsers.includes(user)" class="selected-indicator">✓</span>
        </div>
        <button @click="removeUser(index)"
                class="remove-user-btn"
                :title="`Remove ${user}`"
                :disabled="users.length <= 1">
          <span class="remove-icon">×</span>
        </button>
      </div>
    </div>

    <div class="controls">
      <input v-model="newUser"
             @keyup.enter="addUser"
             placeholder="Add new player"
             class="user-input" />
      <button @click="addUser"
              :disabled="!newUser.trim() || users.includes(newUser.trim())"
              class="add-btn">
        <span class="add-icon">+</span>
        Add
      </button>
    </div>

    <div class="selected-info">
      <div class="selected-count">
        Selected: {{ selectedUsers.length }}/6 players
      </div>
      <div v-if="selectedUsers.length > 0" class="selected-players">
        <div class="selected-title">Playing:</div>
        <div class="selected-list">
          <span v-for="(player, index) in selectedUsers" :key="player" class="selected-player">
            {{ player }}<span v-if="index < selectedUsers.length - 1">, </span>
          </span>
        </div>
      </div>
    </div>

    <div class="action-buttons">
      <button @click="confirm"
              :disabled="selectedUsers.length === 0"
              class="confirm-btn">
        🎮 Start Game ({{ selectedUsers.length }} player{{ selectedUsers.length !== 1 ? 's' : '' }})
      </button>

      <button v-if="selectedUsers.length > 0"
              @click="clearSelection"
              class="clear-btn">
        🗑️ Clear Selection
      </button>
    </div>

    <div v-if="users.length <= 1" class="warning-message">
      ⚠️ You need at least one player to start the game
    </div>

    <!-- Quick Navigation Footer -->
    <div class="navigation-footer">
      <div class="nav-hint">
        💡 Tip: Customize your game questions in the Board Editor before playing!
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'

interface Emits {
  (e: 'players-selected', players: string[]): void
}

const emit = defineEmits<Emits>()

const users = ref<string[]>(['Player 1', 'Player 2', 'Player 3'])
const selectedUsers = ref<string[]>([])
const newUser = ref<string>('')

const toggleUser = (user: string): void => {
  const index = selectedUsers.value.indexOf(user)
  if (index > -1) {
    selectedUsers.value.splice(index, 1)
  } else if (selectedUsers.value.length < 6) {
    selectedUsers.value.push(user)
  }
}

const addUser = (): void => {
  const trimmedUser = newUser.value.trim()
  if (trimmedUser && !users.value.includes(trimmedUser) && users.value.length < 10) {
    users.value.push(trimmedUser)
    newUser.value = ''
  }
}

const removeUser = (index: number): void => {
  if (users.value.length <= 1) return

  const userToRemove = users.value[index]

  const selectedIndex = selectedUsers.value.indexOf(userToRemove)
  if (selectedIndex > -1) {
    selectedUsers.value.splice(selectedIndex, 1)
  }

  users.value.splice(index, 1)
}

const clearSelection = (): void => {
  selectedUsers.value = []
}

const confirm = (): void => {
  if (selectedUsers.value.length > 0) {
    emit('players-selected', [...selectedUsers.value])
  }
}
</script>

<style scoped>
.user-selection {
  background: linear-gradient(to bottom, white, #ff86bd);
  padding: 30px;
  text-align: center;
  min-height: 100vh;
  font-family: 'JetBrains Mono', monospace;
  position: relative;
}

.selection-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 30px;
  flex-wrap: wrap;
  gap: 20px;
}

.selection-header h2 {
  font-size: 36px;
  color: #333;
  text-shadow: 2px 2px 4px rgba(0,0,0,0.1);
  margin: 0;
}

.editor-link {
  background: linear-gradient(135deg, #2196f3, #1976d2);
  color: white;
  text-decoration: none;
  padding: 12px 24px;
  border-radius: 12px;
  font-weight: 500;
  font-size: 16px;
  transition: all 0.3s ease;
  box-shadow: 0 4px 12px rgba(33, 150, 243, 0.3);
  display: flex;
  align-items: center;
  gap: 8px;
  white-space: nowrap;
}

.editor-link:hover {
  background: linear-gradient(135deg, #1976d2, #1565c0);
  transform: translateY(-2px);
  box-shadow: 0 6px 16px rgba(33, 150, 243, 0.4);
}

.user-list {
  display: flex;
  flex-direction: column;
  gap: 12px;
  max-width: 450px;
  margin: 0 auto 30px;
}

.user-item {
  display: flex;
  align-items: center;
  background: rgba(255, 255, 255, 0.9);
  border-radius: 16px;
  overflow: hidden;
  transition: all 0.3s ease;
  border: 2px solid transparent;
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
  backdrop-filter: blur(10px);
}

.user-item.selected {
  background: linear-gradient(135deg, #4fc3f7, #29b6f6);
  color: white;
  border-color: #1976d2;
  box-shadow: 0 6px 20px rgba(25, 118, 210, 0.3);
}

.user-item:hover {
  transform: translateY(-3px);
  box-shadow: 0 8px 25px rgba(0,0,0,0.15);
}

.user-content {
  flex: 1;
  padding: 18px 20px;
  cursor: pointer;
  font-size: 18px;
  text-align: left;
  display: flex;
  align-items: center;
  justify-content: space-between;
  transition: all 0.2s;
}

.user-content:hover {
  background: rgba(255,255,255,0.1);
}

.user-name {
  font-weight: 500;
}

.selected-indicator {
  font-size: 20px;
  font-weight: bold;
  color: #4caf50;
  animation: checkmark 0.3s ease-in-out;
}

@keyframes checkmark {
  0% { transform: scale(0); }
  50% { transform: scale(1.2); }
  100% { transform: scale(1); }
}

.user-item.selected .selected-indicator {
  color: white;
}

.remove-user-btn {
  background: linear-gradient(135deg, #f44336, #d32f2f);
  color: white;
  border: none;
  padding: 12px;
  cursor: pointer;
  font-size: 18px;
  font-weight: bold;
  transition: all 0.3s ease;
  width: 48px;
  height: 48px;
  margin: 4px;
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
  overflow: hidden;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(244, 67, 54, 0.3);
}

.remove-user-btn:hover:not(:disabled) {
  background: linear-gradient(135deg, #d32f2f, #b71c1c);
  transform: translateY(-2px) scale(1.05);
  box-shadow: 0 4px 12px rgba(244, 67, 54, 0.4);
}

.remove-user-btn:disabled {
  background: linear-gradient(135deg, #ccc, #999);
  cursor: not-allowed;
  opacity: 0.6;
  transform: none;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.remove-user-btn::before {
  content: '';
  position: absolute;
  top: 0;
  left: -100%;
  width: 100%;
  height: 100%;
  background: linear-gradient(90deg, transparent, rgba(255,255,255,0.4), transparent);
  transition: left 0.5s;
}

.remove-user-btn:hover::before:not(:disabled) {
  left: 100%;
}

.remove-icon {
  font-size: 20px;
  line-height: 1;
  transition: transform 0.2s;
}

.remove-user-btn:hover .remove-icon:not(:disabled) {
  transform: rotate(90deg) scale(1.1);
}

.controls {
  display: flex;
  gap: 12px;
  justify-content: center;
  margin-bottom: 30px;
  max-width: 450px;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 30px;
}

.user-input {
  flex: 1;
  padding: 14px 18px;
  font-family: 'JetBrains Mono', monospace;
  font-size: 16px;
  border: 2px solid #ddd;
  border-radius: 12px;
  transition: all 0.3s ease;
  background: rgba(255,255,255,0.9);
  backdrop-filter: blur(10px);
  box-shadow: 0 2px 8px rgba(0,0,0,0.05);
}

.user-input:focus {
  outline: none;
  border-color: #4caf50;
  box-shadow: 0 0 0 3px rgba(76, 175, 80, 0.2);
  transform: translateY(-1px);
}

.add-btn {
  padding: 14px 24px;
  background: linear-gradient(135deg, #4caf50, #45a049);
  color: white;
  border: none;
  cursor: pointer;
  font-family: 'JetBrains Mono', monospace;
  font-size: 16px;
  border-radius: 12px;
  transition: all 0.3s ease;
  white-space: nowrap;
  display: flex;
  align-items: center;
  gap: 8px;
  box-shadow: 0 4px 12px rgba(76, 175, 80, 0.3);
}

.add-btn:hover:not(:disabled) {
  background: linear-gradient(135deg, #45a049, #388e3c);
  transform: translateY(-2px);
  box-shadow: 0 6px 16px rgba(76, 175, 80, 0.4);
}

.add-btn:disabled {
  background: linear-gradient(135deg, #ccc, #999);
  cursor: not-allowed;
  transform: none;
  box-shadow: none;
}

.add-icon {
  font-size: 18px;
  font-weight: bold;
}

.selected-info {
  margin-bottom: 30px;
}

.selected-count {
  font-size: 18px;
  color: #555;
  margin-bottom: 15px;
  font-weight: 500;
}

.selected-players {
  background: rgba(255,255,255,0.95);
  padding: 20px;
  border-radius: 16px;
  margin: 0 auto;
  max-width: 450px;
  box-shadow: 0 4px 16px rgba(0,0,0,0.1);
  backdrop-filter: blur(10px);
}

.selected-title {
  font-weight: bold;
  color: #333;
  margin-bottom: 10px;
  font-size: 16px;
}

.selected-list {
  color: #555;
}

.selected-player {
  display: inline-block;
  background: linear-gradient(135deg, #e3f2fd, #bbdefb);
  padding: 4px 12px;
  border-radius: 20px;
  margin: 2px;
  font-size: 14px;
  font-weight: 500;
  color: #1976d2;
}

.action-buttons {
  display: flex;
  justify-content: center;
  gap: 20px;
  flex-wrap: wrap;
  margin-bottom: 30px;
}

.confirm-btn {
  padding: 18px 36px;
  background: linear-gradient(135deg, #c34d7a, #a03d63);
  color: white;
  border: none;
  cursor: pointer;
  font-family: 'JetBrains Mono', monospace;
  font-size: 18px;
  font-weight: 600;
  border-radius: 16px;
  transition: all 0.3s ease;
  min-width: 250px;
  box-shadow: 0 6px 20px rgba(195, 77, 122, 0.3);
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 10px;
}

.confirm-btn:hover:not(:disabled) {
  background: linear-gradient(135deg, #a03d63, #7b1fa2);
  transform: translateY(-3px);
  box-shadow: 0 10px 30px rgba(195, 77, 122, 0.4);
}

.confirm-btn:disabled {
  background: linear-gradient(135deg, #ccc, #999);
  cursor: not-allowed;
  transform: none;
  box-shadow: none;
}

.clear-btn {
  padding: 18px 36px;
  background: linear-gradient(135deg, #ff9800, #f57c00);
  color: white;
  border: none;
  cursor: pointer;
  font-family: 'JetBrains Mono', monospace;
  font-size: 18px;
  font-weight: 600;
  border-radius: 16px;
  transition: all 0.3s ease;
  box-shadow: 0 6px 20px rgba(255, 152, 0, 0.3);
  display: flex;
  align-items: center;
  gap: 10px;
}

.clear-btn:hover {
  background: linear-gradient(135deg, #f57c00, #ef6c00);
  transform: translateY(-3px);
  box-shadow: 0 10px 30px rgba(255, 152, 0, 0.4);
}

.warning-message {
  background: linear-gradient(135deg, #fff3cd, #ffeeba);
  color: #856404;
  padding: 15px 20px;
  border-radius: 12px;
  font-size: 16px;
  font-weight: 500;
  max-width: 450px;
  margin: 0 auto 30px;
  border: 2px solid #ffc107;
  box-shadow: 0 4px 12px rgba(255, 193, 7, 0.2);
}

.navigation-footer {
  margin-top: auto;
  padding-top: 20px;
}

.nav-hint {
  background: rgba(255,255,255,0.8);
  color: #666;
  padding: 12px 20px;
  border-radius: 12px;
  font-size: 14px;
  max-width: 450px;
  margin: 0 auto;
  border: 1px solid rgba(255,255,255,0.5);
  backdrop-filter: blur(8px);
}

/* Responsive design */
@media (max-width: 600px) {
  .selection-header {
    flex-direction: column;
    align-items: center;
    text-align: center;
    gap: 15px;
  }

  .selection-header h2 {
    font-size: 28px;
  }

  .editor-link {
    font-size: 14px;
    padding: 10px 20px;
  }

  .user-list {
    max-width: 100%;
  }

  .controls {
    flex-direction: column;
    max-width: 100%;
  }

  .action-buttons {
    flex-direction: column;
    align-items: center;
  }

  .confirm-btn, .clear-btn {
    width: 100%;
    max-width: 320px;
  }

  .selected-players {
    max-width: 100%;
  }

  .warning-message {
    max-width: 100%;
  }

  .nav-hint {
    max-width: 100%;
  }
}

@media (max-width: 480px) {
  .selection-header h2 {
    font-size: 24px;
  }

  .user-content {
    padding: 15px;
    font-size: 16px;
  }

  .remove-user-btn {
    width: 42px;
    height: 42px;
    border-radius: 10px;
  }

  .remove-icon {
    font-size: 18px;
  }
}
</style>
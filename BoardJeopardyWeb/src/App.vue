<template>
  <div class="min-h-screen bg-gradient-to-br from-blue-900 via-purple-900 to-indigo-900 p-4">
    <!-- Score Board -->
    <div v-if="gameStarted" class="bg-gradient-to-r from-yellow-400 to-yellow-600 p-8 rounded-xl shadow-2xl mb-8 border-4 border-yellow-300">
      <h1 class="text-7xl font-black text-blue-900 text-center mb-8 tracking-wider drop-shadow-lg">JEOPARDY!</h1>

      <div v-if="selectedUsers.length === 2" class="grid grid-cols-2 gap-8">
        <div class="text-center bg-white rounded-xl p-6 shadow-lg">
          <h2 class="text-3xl font-bold text-blue-900 mb-4">{{ selectedUsers[0] }}</h2>
          <div
              class="text-5xl font-black text-green-600 bg-gradient-to-br from-gray-50 to-gray-100 p-6 rounded-xl cursor-pointer select-none hover:shadow-lg transition-all border-4 border-gray-200 hover:border-green-300"
              @click.left="adjustScore(0, 200)"
              @click.right.prevent="adjustScore(0, -200)"
          >
            ${{ scores[0].toLocaleString() }}
          </div>
        </div>
        <div class="text-center bg-white rounded-xl p-6 shadow-lg">
          <h2 class="text-3xl font-bold text-blue-900 mb-4">{{ selectedUsers[1] }}</h2>
          <div
              class="text-5xl font-black text-green-600 bg-gradient-to-br from-gray-50 to-gray-100 p-6 rounded-xl cursor-pointer select-none hover:shadow-lg transition-all border-4 border-gray-200 hover:border-green-300"
              @click.left="adjustScore(1, 200)"
              @click.right.prevent="adjustScore(1, -200)"
          >
            ${{ scores[1].toLocaleString() }}
          </div>
        </div>
      </div>

      <div v-else-if="selectedUsers.length === 3" class="grid grid-cols-3 gap-6">
        <div v-for="(user, index) in selectedUsers" :key="index" class="text-center bg-white rounded-xl p-6 shadow-lg">
          <h2 class="text-2xl font-bold text-blue-900 mb-4">{{ user }}</h2>
          <div
              class="text-4xl font-black text-green-600 bg-gradient-to-br from-gray-50 to-gray-100 p-6 rounded-xl cursor-pointer select-none hover:shadow-lg transition-all border-4 border-gray-200 hover:border-green-300"
              @click.left="adjustScore(index, 200)"
              @click.right.prevent="adjustScore(index, -200)"
          >
            ${{ scores[index].toLocaleString() }}
          </div>
        </div>
      </div>
    </div>

    <!-- Game Board -->
    <div class="bg-gradient-to-br from-blue-800 to-blue-900 p-8 rounded-xl shadow-2xl border-4 border-blue-700">
      <!-- Categories -->
      <div class="grid grid-cols-6 gap-3 mb-6">
        <button
            v-for="(category, index) in categories"
            :key="index"
            class="bg-gradient-to-br from-blue-600 to-blue-700 hover:from-blue-500 hover:to-blue-600 text-white font-black py-6 px-4 rounded-lg text-xl shadow-lg transform hover:scale-105 transition-all duration-200 border-2 border-blue-500 hover:border-blue-400"
            @click="editCategory(index)"
        >
          {{ category }}
        </button>
      </div>

      <!-- Question Grid -->
      <div class="grid grid-cols-6 gap-3">
        <div v-for="col in 6" :key="col" class="grid grid-rows-5 gap-3">
          <button
              v-for="row in 5"
              :key="row"
              :disabled="!questions[getQuestionIndex(col-1, row-1)].enabled"
              :class="[
              'font-black py-6 px-4 rounded-lg text-2xl shadow-lg transform transition-all duration-200 border-2',
              questions[getQuestionIndex(col-1, row-1)].enabled 
                ? 'bg-gradient-to-br from-yellow-400 to-yellow-600 hover:from-yellow-300 hover:to-yellow-500 text-blue-900 cursor-pointer hover:scale-105 border-yellow-300 hover:border-yellow-200' 
                : 'bg-gradient-to-br from-gray-700 to-gray-800 text-gray-500 cursor-not-allowed border-gray-600'
            ]"
              @click="showQuestion(col-1, row-1)"
          >
            ${{ (row * 200).toLocaleString() }}
          </button>
        </div>
      </div>
    </div>

    <!-- User Selection Modal -->
    <div v-if="!gameStarted" class="fixed inset-0 bg-black bg-opacity-75 flex items-center justify-center p-4">
      <div class="bg-gradient-to-br from-white to-gray-100 p-10 rounded-2xl max-w-lg w-full shadow-2xl border-4 border-blue-300">
        <h2 class="text-4xl font-black text-blue-900 mb-8 text-center">Select Players</h2>
        <div class="space-y-4 mb-8">
          <div v-for="(user, index) in availableUsers" :key="index" class="flex items-center bg-white p-4 rounded-xl shadow-md hover:shadow-lg transition-all">
            <input
                type="checkbox"
                :id="`user-${index}`"
                v-model="userSelection[index]"
                class="mr-4 w-5 h-5 text-blue-600 rounded"
            >
            <label :for="`user-${index}`" class="text-xl font-bold text-blue-900 cursor-pointer flex-1">{{ user }}</label>
          </div>
        </div>
        <button
            @click="startGame"
            :disabled="selectedUserCount < 2"
            class="bg-gradient-to-r from-blue-600 to-blue-700 hover:from-blue-700 hover:to-blue-800 disabled:from-gray-400 disabled:to-gray-500 text-white font-black py-4 px-8 rounded-xl w-full text-xl shadow-lg transform hover:scale-105 transition-all duration-200"
        >
          Start Game
        </button>
        <p class="text-center text-sm text-gray-600 mt-4">Select at least 2 players to start</p>
      </div>
    </div>

    <!-- Question Modal -->
    <div v-if="currentQuestion" class="fixed inset-0 bg-black bg-opacity-80 flex items-center justify-center p-4">
      <div class="bg-gradient-to-br from-white to-blue-50 rounded-2xl max-w-4xl w-full shadow-2xl border-4 border-blue-300">
        <div class="p-8">
          <div class="flex justify-between items-center mb-8">
            <h3 class="text-3xl font-black text-blue-900">{{ currentQuestionTitle }}</h3>
            <button @click="closeQuestion" class="text-gray-500 hover:text-red-600 text-4xl font-bold transition-colors">&times;</button>
          </div>

          <!-- Text Question -->
          <div v-if="currentQuestion.type === 'Text'" class="text-center py-12 bg-gradient-to-br from-blue-600 to-blue-700 rounded-xl text-white shadow-lg">
            <p class="text-4xl font-bold leading-relaxed px-8">{{ showAnswer ? currentQuestion.answer : currentQuestion.question }}</p>
          </div>

          <!-- Image Question -->
          <div v-else-if="currentQuestion.type === 'Image'" class="text-center py-8 bg-gradient-to-br from-blue-600 to-blue-700 rounded-xl shadow-lg">
            <img :src="currentQuestion.question" alt="Question Image" class="max-w-full h-auto mx-auto rounded-lg shadow-lg">
          </div>

          <!-- Audio Question -->
          <div v-else-if="currentQuestion.type === 'Audio'" class="text-center py-12 bg-gradient-to-br from-blue-600 to-blue-700 rounded-xl shadow-lg">
            <audio controls class="mx-auto w-full max-w-md">
              <source :src="currentQuestion.question" type="audio/mpeg">
            </audio>
          </div>
        </div>

        <div class="bg-gradient-to-r from-gray-100 to-gray-200 px-8 py-6 rounded-b-2xl flex justify-between">
          <button
              @click="toggleAnswer"
              class="bg-gradient-to-r from-green-600 to-green-700 hover:from-green-700 hover:to-green-800 text-white font-black py-3 px-8 rounded-xl shadow-lg transform hover:scale-105 transition-all text-lg"
          >
            {{ showAnswer ? 'Show Question' : 'Show Answer' }}
          </button>
          <button
              @click="closeQuestion"
              class="bg-gradient-to-r from-blue-600 to-blue-700 hover:from-blue-700 hover:to-blue-800 text-white font-black py-3 px-8 rounded-xl shadow-lg transform hover:scale-105 transition-all text-lg"
          >
            Close
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'

const gameStarted = ref(false)
const categories = ref([
  'Category 1', 'Category 2', 'Category 3',
  'Category 4', 'Category 5', 'Category 6'
])
const availableUsers = ref(['Player 1', 'Player 2', 'Player 3'])
const userSelection = ref([false, false, false])
const selectedUsers = ref([])
const scores = ref([])
const currentQuestion = ref(null)
const currentQuestionTitle = ref('')
const showAnswer = ref(false)

const questions = ref(Array.from({ length: 30 }, (_, i) => ({
  question: `Question ${i + 1}`,
  answer: `Answer ${i + 1}`,
  type: 'Text',
  enabled: true
})))

const selectedUserCount = computed(() =>
    userSelection.value.filter(selected => selected).length
)

const getQuestionIndex = (col, row) => col * 5 + row

const startGame = () => {
  selectedUsers.value = availableUsers.value.filter((_, index) => userSelection.value[index])
  scores.value = new Array(selectedUsers.value.length).fill(0)
  gameStarted.value = true
  loadData()
}

const adjustScore = (playerIndex, amount) => {
  scores.value[playerIndex] += amount
}

const showQuestion = (col, row) => {
  const questionIndex = getQuestionIndex(col, row)
  const question = questions.value[questionIndex]

  if (!question.enabled) return

  currentQuestion.value = question
  currentQuestionTitle.value = `${categories.value[col]} - $${(row + 1) * 200}`
  showAnswer.value = false
  question.enabled = false
}

const toggleAnswer = () => {
  showAnswer.value = !showAnswer.value
}

const closeQuestion = () => {
  currentQuestion.value = null
  currentQuestionTitle.value = ''
  showAnswer.value = false
}

const editCategory = (index) => {
  const newCategory = prompt('Enter category name:', categories.value[index])
  if (newCategory) {
    categories.value[index] = newCategory
  }
}

const loadData = async () => {
  // Load from localStorage or API
  const savedCategories = localStorage.getItem('jeopardy-categories')
  const savedQuestions = localStorage.getItem('jeopardy-questions')
  const savedUsers = localStorage.getItem('jeopardy-users')

  if (savedCategories) {
    categories.value = JSON.parse(savedCategories)
  }
  if (savedQuestions) {
    questions.value = JSON.parse(savedQuestions)
  }
  if (savedUsers) {
    availableUsers.value = JSON.parse(savedUsers)
  }
}

const saveData = () => {
  localStorage.setItem('jeopardy-categories', JSON.stringify(categories.value))
  localStorage.setItem('jeopardy-questions', JSON.stringify(questions.value))
  localStorage.setItem('jeopardy-users', JSON.stringify(availableUsers.value))
}

onMounted(() => {
  loadData()
})
</script>
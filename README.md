# 🎯 Jeopardy

A feature-rich Jeopardy game built as a native desktop app with **Electron**, **Vue 3**, and **TypeScript**. Supports custom boards, multiple question types, theming, and live score tracking.

**Current version: 1.2.0**

---

## Features

- 🎮 **Game Board** — fully interactive Jeopardy board that scales with window size
- ✏️ **Board Editor** — create and edit categories and questions with support for text, image, audio, and YouTube video question types
- 🏆 **Scoreboard** — live score tracking with the ability to award points to players
- 👥 **Player Management** — add/remove players before and during the game
- 🎨 **Theming** — light, dark, and system theme modes with fully customisable accent and background colours
- 📺 **YouTube Questions** — embed YouTube videos directly as questions with start time and clip length support
- 💾 **Persistent State** — board data and player lists are saved locally between sessions

---

## Getting Started

### Prerequisites
- [Node.js](https://nodejs.org/) v20.19.0 or newer

### Install & Run

```sh
cd jeopardy-game
npm install

# Run as desktop app (Electron)
npm run electron:dev

# Or run in browser
npm run dev
```

### Build for distribution

```sh
cd jeopardy-game
npm run electron:build
```

Distributable files are output to `jeopardy-game/release/`.

---

## How to Play

1. Launch the app and go to the **Editor** tab to set up your board — add category names and fill in questions
2. Switch to the **Game** tab, select your players, and hit **Start Game**
3. Click a point value on the board to reveal a question
4. Use **Show Answer** to reveal the answer, then award points to the correct player
5. Track scores on the **Scoreboard** view

> **Tip:** The board is saved automatically — you only need to set it up once.

---

## Tech Stack

- [Vue 3](https://vuejs.org/) + [TypeScript](https://www.typescriptlang.org/)
- [Vite](https://vite.dev/)
- [Pinia](https://pinia.vuejs.org/) — state management
- [Vue Router](https://router.vuejs.org/)
- [Electron](https://www.electronjs.org/) — desktop packaging

---

## Release Notes

See [RELEASE_NOTES.md](./RELEASE_NOTES.md) for a full changelog.

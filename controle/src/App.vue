<script setup lang="ts">
import { RouterView } from 'vue-router'
import AppNavButton from './components/AppNavButton.vue'
</script>

<template>
  <header class="flex p-2 bg-black w-full justify-center">
    <div class="container">
      <div class="flex justify-center items-center gap-10">
        <img src="./assets/logo.png" class="w-24" />
        <div class="text-4xl font-bold text-white">Hard Country 2024</div>
        <nav class="flex gap-4">
          <AppNavButton to="/" label="Inserir Números" />
          <AppNavButton to="/excluir_numeros" label="Excluir Números" />
        </nav>
      </div>
    </div>
  </header>
  <div class="flex w-full justify-center bg-gray-900">
    <div class="flex w-full justify-center container py-4">
      <RouterView />
    </div>
  </div>
</template>

<style scoped></style>

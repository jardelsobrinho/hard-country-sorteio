<script setup lang="ts">
import { RouterView } from 'vue-router'
import AppNavButton from './components/AppNavButton.vue'
</script>

<template>
  <header class="flex p-2 bg-black w-full justify-center">
    <div class="container">
      <nav class="flex gap-4">
        <AppNavButton to="/" label="Inserir Números" />
        <AppNavButton to="/excluir_numeros" label="Excluir Números" />
      </nav>
    </div>
  </header>
  <div class="flex w-full justify-center bg-black">
    <div class="flex w-full justify-center container py-4">
      <RouterView />
    </div>
  </div>
</template>

<style scoped></style>

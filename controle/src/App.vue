<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <header class="flex p-2 bg-black w-full justify-center">
    <div class="container">
      <nav class="flex gap-4">
        <div class="p-4 bg-slate-500 rounded">
          <RouterLink to="/">Inserir Números</RouterLink>
        </div>
        <div class="p-4 bg-slate-500 rounded">
          <RouterLink to="/numeros_sorteados">Excluir Números</RouterLink>
        </div>
      </nav>
    </div>
  </header>
  <div class="flex w-full justify-center bg-black">
    <div class="container py-4">
      <RouterView />
    </div>
  </div>
</template>

<style scoped></style>

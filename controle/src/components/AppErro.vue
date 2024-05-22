<template>
  <div class="flex flex-col p-4 bg-red-400" v-if="model != ''">
    <div class="text-2xl font-bold">
      {{ model }}
    </div>
  </div>
</template>

<script setup lang="ts">
const model = defineModel()
</script>

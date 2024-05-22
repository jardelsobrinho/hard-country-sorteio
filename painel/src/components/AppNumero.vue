<template>
  <div class="w-10 text-center h-10 px-1 py-2 font-bold" :class="[classNumero]">
    {{ props.numero }}
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
interface IProps {
  numero: number
  sorteado: boolean
  vencedor: boolean
}

const props = defineProps<IProps>()

const classNumero = computed(() => {
  if (props.vencedor) {
    return 'bg-green-600 text-white border border-yellow-300'
  } else if (props.sorteado) {
    return 'bg-red-700 text-white'
  } else {
    return 'bg-white text-black'
  }
})
</script>

<template>
  <RouterLink :to="props.to">
    <div
      class="py-2 px-4"
      :class="[linkAtual ? 'bg-gray-800 text-white' : 'border border-gray-800 text-gray-400']"
    >
      {{ props.label }}
    </div>
  </RouterLink>
</template>

<script setup lang="ts">
import { onMounted, watch, ref } from 'vue'
import { RouterLink, useRoute } from 'vue-router'
interface IProps {
  label: string
  to: string
}

const props = defineProps<IProps>()
const route = useRoute()
const linkAtual = ref(false)

watch(
  () => route.path,
  (newPath) => {
    linkAtual.value = newPath == props.to
  }
)

onMounted(() => {
  linkAtual.value = route.path == props.to
})
</script>

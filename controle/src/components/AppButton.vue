<template>
  <button
    class="bg-green-400 py-4 px-8 rounded-full text-3xl font-bold text-gray-800 hover:bg-green-300"
    :disabled="props.disabled"
    @click="emit('onClick')"
  >
    {{ props.label }}
  </button>
</template>

<script setup lang="ts">
interface IProps {
  label: string
  disabled: boolean
}
const props = defineProps<IProps>()

const emit = defineEmits<{
  (event: 'onClick'): void
}>()
</script>

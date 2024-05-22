<template>
  <div class="text-5xl text-white font-bold">{{ props.title }}</div>
</template>

<script setup lang="ts">
interface IProps {
  title: string
}

const props = defineProps<IProps>()
</script>

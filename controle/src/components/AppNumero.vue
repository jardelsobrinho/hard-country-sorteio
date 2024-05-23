<template>
  <div class="bg-white w-10 text-center h-10 px-1 py-2 font-bold cursor-pointer" @click="onExcluir">
    {{ props.numero }}
  </div>
</template>

<script setup lang="ts">
import axios from 'axios'
import { client } from '../services/ClientService'
interface IProps {
  numero: number
}

const props = defineProps<IProps>()

const emit = defineEmits<{
  (event: 'onDepoisExcluir'): void
  (event: 'onError', payload: string): void
}>()

const onExcluir = async () => {
  if (confirm('Tem certeza que deseja excluir o número ' + props.numero + ' ?')) {
    try {
      await client.delete('api/Sorteio/' + props.numero)
      alert('Número ' + props.numero + ' excluído com sucesso!')
    } catch (error) {
      if (axios.isAxiosError(error)) {
        if (error.response?.status == 405) {
          emit('onError', error.response?.statusText)
        } else {
          emit('onError', error.response?.data.title)
        }

        console.log(error.response)
      } else {
        alert(error)
      }
    }
    emit('onDepoisExcluir')
  }
}
</script>

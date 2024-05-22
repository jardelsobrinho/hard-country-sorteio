<template>
  <div class="flex flex-col gap-2">
    <AppTitle title="EXCLUIR NÚMEROS" />
    <AppErro v-model="erroApi" />
    <div class="container">
      <div class="flex flex-wrap gap-1">
        <AppNumero
          v-for="n in numerosSorteados"
          :key="n.numero"
          :numero="n.numero"
          @on-depois-excluir="atualizaListaNumeros"
          @on-error="onExcluirError"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import AppTitle from '@/components/AppTitle.vue'
import AppErro from '@/components/AppErro.vue'
import AppNumero from '@/components/AppNumero.vue'
import { client } from '../services/ClientService'
import axios from 'axios'

interface INumeroSorteado {
  numero: number
  vencedor: boolean
}

const numerosSorteados = ref<INumeroSorteado[]>([])
const erroApi = ref('')
const onExcluirError = (error: string) => {
  erroApi.value = error
  setTimeout(() => {
    erroApi.value = ''
  }, 3000)
}

const atualizaListaNumeros = async () => {
  try {
    numerosSorteados.value = (await client.get<INumeroSorteado[]>('api/Sorteio')).data
  } catch (error) {
    if (axios.isAxiosError(error)) {
      erroApi.value = error.response?.data
      console.log(error.response?.data)
    } else {
      alert(error)
    }
  }
}

onMounted(async () => {
  await atualizaListaNumeros()
})
</script>

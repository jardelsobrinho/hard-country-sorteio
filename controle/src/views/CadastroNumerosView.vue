<template>
  <div class="flex flex-col gap-2">
    <AppTitle title="CADASTRO DE NÚMEROS" />
    <div class="flex flex-row gap-4 justify- items-end pt-4">
      <div class="flex flex-col gap-4">
        <label class="text-white text-3xl">Número Sorteado</label>
        <input
          class="bg-slate-300 border-solid rounded-xl text-3xl font-bold p-4"
          maxlength="3"
          type="number"
          v-model.number="numeroSorteado"
          :disabled="disabled"
        />
      </div>

      <div>
        <AppButton :disabled="disabled" @on-click="onAdicionar" label="Adicionar" />
      </div>
    </div>
    <AppErro v-model="erroApi" />
    <div class="flex flex-col p-4 bg-green-500" v-if="sucessoApi">
      <div class="text-2xl font-bold">Numero cadastrado com sucesso!!</div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import axios from 'axios'
import { client } from '../services/ClientService'
import AppButton from '@/components/AppButton.vue'
import AppTitle from '@/components/AppTitle.vue'
import AppErro from '@/components/AppErro.vue'

const numeroSorteado = ref(0)
const erroApi = ref('')
const sucessoApi = ref(false)
const disabled = ref(false)

watch(numeroSorteado, (newValue) => {
  erroApi.value = ''
  let valorAux = newValue.toString()
  if (valorAux.length > 3) {
    valorAux = valorAux.substring(0, 3)
  }
  numeroSorteado.value = parseInt(valorAux)
})

const onAdicionar = async () => {
  sucessoApi.value = false
  disabled.value = true
  try {
    await client.post('api/Sorteio/' + numeroSorteado.value)
    numeroSorteado.value = 0
    sucessoApi.value = true
    setTimeout(() => {
      sucessoApi.value = false
    }, 3000)
  } catch (error) {
    if (axios.isAxiosError(error)) {
      erroApi.value = error.response?.data.title
      console.log(error.response?.data)
    } else {
      alert(error)
    }
  }
  disabled.value = false
}
</script>

<template>
  <div class="flex w-full h-screen bg-black justify-center">
    <div class="flex flex-col container gap-2 items-center">
      <div class="flex flex-row items-center gap-4 justify-between">
        <img src="./assets/logo.png" class="w-24" />
        <AppTitle title="SORTEIO HARD COUNTRY 2024" />
      </div>
      <AppErro v-model="erroApi" />
      <div class="flex flex-wrap gap-1">
        <AppNumero
          v-for="n in numeros"
          :key="n.numero"
          :numero="n.numero"
          :sorteado="n.sorteado"
          :vencedor="n.vencedor"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import AppTitle from './components/AppTitle.vue'
import AppNumero from './components/AppNumero.vue'
import AppErro from './components/AppErro.vue'
import { HubConnectionBuilder } from '@microsoft/signalr'
import { onMounted, ref, watch } from 'vue'
import axios from 'axios'
import { client } from './services/ClientService'

interface INumeroSorteado {
  numero: number
  vencedor: boolean
  sorteado: boolean
}

const numerosSorteados = ref<INumeroSorteado[]>([])
const numeros = ref<INumeroSorteado[]>([])
const erroApi = ref('')
const connection = new HubConnectionBuilder()
  .withUrl('http://localhost:7000/notificationHub')
  .build()

watch(numerosSorteados, () => {
  numerosSorteados.value?.forEach((numSorteado) => {
    for (const num of numeros.value) {
      if (num.numero == numSorteado.numero) {
        num.sorteado = true
        num.vencedor = numSorteado.vencedor
        break
      }
    }
  })

  let listaNumerosNaoSorteados = [...numeros.value]
  listaNumerosNaoSorteados = listaNumerosNaoSorteados.filter((x) => {
    let remover = true
    for (const numSort of numerosSorteados.value) {
      if (numSort.numero == x.numero) {
        remover = false
        break
      }
    }
    return remover
  })

  listaNumerosNaoSorteados.forEach((numNaoSorteado) => {
    for (const num of numeros.value) {
      if (num.numero == numNaoSorteado.numero) {
        num.sorteado = false
        break
      }
    }
  })
})

const atualizaListaNumeros = async () => {
  try {
    numerosSorteados.value = (await client.get<INumeroSorteado[]>('api/Sorteio')).data
  } catch (error) {
    if (axios.isAxiosError(error)) {
      if (error.code == 'ERR_NETWORK') {
        erroApi.value = error.message
      } else {
        erroApi.value = error.response?.data.title
      }
      console.log(error)
    } else {
      alert(error)
    }
  }
}

const criarNumerosSorteio = () => {
  var lista: INumeroSorteado[] = []
  for (var i = 1; i <= 300; i++) {
    const numero: INumeroSorteado = {
      numero: i,
      vencedor: false,
      sorteado: false
    }
    lista.push(numero)
  }

  numeros.value = [...lista]
}

const connectSignalR = () => {
  connection
    .start()
    .then(() => {
      connection.on('UpdateNumerosSorteados', (action) => {
        numerosSorteados.value = action
      })
    })
    .catch((error) => {
      alert(error)
      erroApi.value = 'Erro SignalR: ' + error
    })

  connection.onclose(() => {
    connectSignalR()
  })
}

onMounted(async () => {
  criarNumerosSorteio()
  await atualizaListaNumeros()

  connectSignalR()
})
</script>

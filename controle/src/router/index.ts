import { createRouter, createWebHistory } from 'vue-router'

import CadastroNumerosView from '../views/CadastroNumerosView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: CadastroNumerosView
    },
    {
      path: '/numeros_sorteados',
      name: 'numeros_sorteados',
      component: () => import('../views/NumerosSorteadosView.vue')
    }
  ]
})

export default router

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
      path: '/excluir_numeros',
      name: 'excluir_numeros',
      component: () => import('../views/ExcluirNumerosView.vue')
    }
  ]
})

export default router

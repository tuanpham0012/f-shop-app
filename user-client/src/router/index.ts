import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/:categoryCode',
      name: 'Category',
      component: () => import('../views/Product/FindProductByCategory.vue')
      
    },
  ]
})

export default router

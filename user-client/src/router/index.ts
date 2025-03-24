import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('../views/Home/HomeView.vue')
    },
    {
      path: '/danh-muc/:categoryCode',
      name: 'Category',
      component: () => import('../views/Product/FindProductByCategory.vue')
      
    },
  ]
})

export default router

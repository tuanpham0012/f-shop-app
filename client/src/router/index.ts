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
      path: '/customers',
      name: 'customers',
      component: () => import('../views/Customer/Customer.vue')
    },
    {
      path: '/menus',
      name: 'menus',
      component: () => import('../views/Menu/Menu.vue')
    },
    {
      path: '/',
      name: 'product',
      children: [
        {
          path: '/products',
          name: 'Products',
          component: () => import('../views/Product/Product.vue')
        },
        {
          path: '/brands',
          name: 'Brands',
          component: () => import('../views/Brand/Brand.vue')
        },
      ]
      
    },
  ]
})

export default router

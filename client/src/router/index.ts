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
      path: '/',
      name: 'setting',
      children: [
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
          path: '/taxes',
          name: 'taxes',
          component: () => import('../views/Tax/Tax.vue')
        },
      ]
      
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
        {
          path: '/categories',
          name: 'Categories',
          component: () => import('../views/Category/Category.vue')
        },
      ]
      
    },
    {
      path: '/',
      name: 'orders',
      children: [
        {
          path: '/orders',
          name: 'order-list',
          component: () => import('../views/Order/Order.vue')
        }
      ]
      
    },
  ]
})

export default router

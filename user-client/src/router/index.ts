import { createRouter, createWebHistory } from 'vue-router'

const routes = [
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
  {
    path: '/san-pham/:productCode',
    name: 'ProductDetail',
    component: () => import('../views/Product/ProductViewDetail.vue')
    
  },
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: routes,
  scrollBehavior(to, from, savedPosition) {
    // savedPosition chỉ tồn tại khi người dùng nhấn nút back/forward của trình duyệt
    if (savedPosition) {
      return savedPosition
    } else {
      // Luôn cuộn lên đầu trang khi điều hướng đến route mới
      return { top: 0, left: 0, behavior: 'smooth' } // Hoặc chỉ { top: 0 } nếu không cần smooth
    }
  }
})

export default router

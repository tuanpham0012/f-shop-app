<script setup lang="ts">
import { ref } from 'vue'
import TabNavigation from '@/components/order-components/TabNavigation.vue'
import ProductCard from '@/components/order-components/ProductCard.vue'

const activeTab = ref('all')

interface Product {
  id: number
  image: string
  title: string
  variant?: string
  price: number
  originalPrice?: number
  quantity: number
  seller: string
  hasChat: boolean
  hasShop: boolean
  status: 'pending' | 'shipped' | 'delivered' | 'cancelled'
}

const products = ref<Product[]>([
  {
    id: 1,
    image: 'https://images.pexels.com/photos/1191531/pexels-photo-1191531.jpeg?auto=compress&cs=tinysrgb&w=300',
    title: '64mm - 53mm Vòng Miếng Vòng Tay Phong Cách Mới Phụ Kiện Một Bước Một Vòng Vòng Tay Nữ Thủy Tinh Giả Ngọc Jingle Vòng Tay',
    variant: 'Phân loại hàng: 6#白色,36mm (1Pc)',
    price: 20900,
    quantity: 1,
    seller: 'qingdian',
    hasChat: true,
    hasShop: true,
    status: 'delivered'
  },
  {
    id: 2,
    image: 'https://images.pexels.com/photos/1191531/pexels-photo-1191531.jpeg?auto=compress&cs=tinysrgb&w=300',
    title: '64mm - 53mm Vòng Miếng Vòng Tay Phong Cách Mới Phụ Kiện Một Bước Một Vòng Vòng Tay Nữ Thủy Tinh Giả Ngọc Jingle Vòng Tay',
    variant: 'Phân loại hàng: 3#紫色,64mm (1Pc)',
    price: 20900,
    quantity: 1,
    seller: 'qingdian',
    hasChat: true,
    hasShop: true,
    status: 'delivered'
  },
  {
    id: 3,
    image: 'https://images.pexels.com/photos/205421/pexels-photo-205421.jpeg?auto=compress&cs=tinysrgb&w=300',
    title: 'Giá Đỡ Lap - Macbook, Ipad Gập Gọn Kê Để Laptop Bằng Nhựa ABS 4.9, Có Thể Điều Chỉnh Được Độ Cao, Độ Tản Nhiệt Laptop',
    variant: '',
    price: 447500,
    originalPrice: 955000,
    quantity: 1,
    seller: 'anhtungsmart',
    hasChat: true,
    hasShop: true,
    status: 'delivered'
  }
])

const orderTotals = ref([
  { id: 1, total: 937120 },
  { id: 2, total: 442180 }
])
</script>

<template>
  <div class="order-history mt-5">
    <div class="container">
      <TabNavigation 
        :active-tab="activeTab" 
        @tab-change="activeTab = $event" 
      />
      
      <div class="orders-container">
        <!-- Order 1 -->
        <div class="order-group">
          <div class="order-header">
            <span class="seller-name">Mã đơn hàng: #123456</span>
            <div class="seller-actions">
              <button class="chat-btn">
                <span class="chat-icon">💬</span> Chat
              </button>
              <button class="shop-btn">
                <span class="shop-icon">🏪</span> Xem Shop
              </button>
            </div>
            <div class="delivery-status">
              <span class="delivery-icon">📦</span>
              <span class="delivery-text">Giao hàng thành công</span>
              <span class="completion-badge">HOÀN THÀNH</span>
            </div>
          </div>
          
          <ProductCard 
            v-for="product in products.slice(0, 2)" 
            :key="product.id"
            :product="product"
          />
          
          <div class="order-total">
            <span class="total-label">Thành tiền:</span>
            <span class="total-amount">₫937.120</span>
          </div>
          
          <div class="order-actions">
            <div class="order-date">
              <p>Đánh giá sản phẩm trước 07-07-2025</p>
              <p>Đánh giá ngay và nhận 200 Xu</p>
            </div>
            <div class="action-buttons">
              <button class="rate-btn">Đánh Giá</button>
              <button class="rebuy-btn">Mua Lại</button>
            </div>
          </div>
        </div>

        <!-- Order 2 -->
        <div class="order-group">
          <div class="order-header">
            <span class="seller-badge">Yêu thích</span>
            <span class="seller-name">anhtungsmart</span>
            <div class="seller-actions">
              <button class="chat-btn">
                <span class="chat-icon">💬</span> Chat
              </button>
              <button class="shop-btn">
                <span class="shop-icon">🏪</span> Xem Shop
              </button>
            </div>
            <div class="delivery-status">
              <span class="delivery-icon">📦</span>
              <span class="delivery-text">Giao hàng thành công</span>
              <span class="completion-badge">HOÀN THÀNH</span>
            </div>
          </div>
          
          <ProductCard 
            :product="products[2]"
          />
          
          <div class="order-total">
            <span class="total-label">Thành tiền:</span>
            <span class="total-amount">₫442.180</span>
          </div>
          
          <div class="order-actions">
            <div class="order-date">
              <p>Đánh giá sản phẩm trước 05-07-2025</p>
              <p>Đánh giá ngay và nhận 200 Xu</p>
            </div>
            <div class="action-buttons">
              <button class="rate-btn">Đánh Giá</button>
              <button class="contact-btn">Liên Hệ Người Bán</button>
              <button class="rebuy-btn">Mua Lại</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.order-history {
  min-height: 100vh;
  background-color: #f5f5f5;
}

.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 16px;
}

.orders-container {
  margin-top: 24px;
}

.order-group {
  background: white;
  border-radius: 8px;
  margin-bottom: 16px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  overflow: hidden;
}

.order-header {
  padding: 16px 20px;
  border-bottom: 1px solid #f0f0f0;
  display: flex;
  align-items: center;
  gap: 12px;
  flex-wrap: wrap;
}

.seller-badge {
  background: var(--bs-primary);
  color: white;
  padding: 4px 8px;
  border-radius: 4px;
  font-size: 12px;
  font-weight: 500;
}

.seller-name {
  font-weight: 600;
  color: #333;
}

.seller-actions {
  display: flex;
  gap: 8px;
  margin-left: auto;
}

.chat-btn, .shop-btn {
  border: 1px solid #e0e0e0;
  background: white;
  padding: 6px 12px;
  border-radius: 4px;
  font-size: 12px;
  cursor: pointer;
  display: flex;
  align-items: center;
  gap: 4px;
  transition: all 0.2s;
}

.chat-btn:hover, .shop-btn:hover {
  border-color: var(--bs-primary);
  color: var(--bs-primary);
}

.delivery-status {
  display: flex;
  align-items: center;
  gap: 8px;
  width: 100%;
  margin-top: 8px;
}

.delivery-text {
  color: #52c41a;
  font-size: 14px;
}

.completion-badge {
  background: var(--bs-primary);
  color: white;
  padding: 4px 8px;
  border-radius: 4px;
  font-size: 12px;
  font-weight: 500;
  margin-left: auto;
}

.order-total {
  padding: 16px 20px;
  text-align: right;
  border-top: 1px solid #f0f0f0;
  display: flex;
  justify-content: flex-end;
  align-items: center;
  gap: 8px;
}

.total-label {
  color: #666;
  font-size: 16px;
}

.total-amount {
  color: var(--bs-primary);
  font-size: 20px;
  font-weight: 600;
}

.order-actions {
  padding: 16px 20px;
  border-top: 1px solid #f0f0f0;
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  gap: 16px;
}

.order-date p{
  color: #666;
  font-size: 12px;
}

.action-buttons {
  display: flex;
  gap: 12px;
}

.rate-btn, .contact-btn, .rebuy-btn {
  padding: 8px 16px;
  border-radius: 4px;
  font-size: 14px;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.2s;
  border: 1px solid;
}

.rate-btn {
  background: var(--bs-primary);
  color: white;
  border-color: var(--bs-primary);
}

.rate-btn:hover {
  background: #e64919;
  border-color: #e64919;
}

.contact-btn, .rebuy-btn {
  background: white;
  color: #666;
  border-color: #e0e0e0;
}

.contact-btn:hover, .rebuy-btn:hover {
  border-color: var(--bs-primary);
  color: var(--bs-primary);
}

@media (max-width: 768px) {
  .container {
    padding: 0 12px;
  }
  
  .order-header {
    padding: 12px 16px;
    flex-direction: column;
    align-items: flex-start;
    gap: 8px;
  }
  
  .seller-actions {
    margin-left: 0;
    order: -1;
    align-self: flex-end;
  }
  
  .delivery-status {
    margin-top: 0;
  }
  
  .order-actions {
    flex-direction: column;
    align-items: flex-start;
    gap: 12px;
  }
  
  .action-buttons {
    width: 100%;
    justify-content: space-between;
  }
  
  .rate-btn, .contact-btn, .rebuy-btn {
    flex: 1;
    min-width: 0;
    font-size: 12px;
    padding: 6px 8px;
  }
}
</style>
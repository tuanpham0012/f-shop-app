<script setup lang="ts">

const props = defineProps({
  product: {
    type: Object as any,
    default: null
  }
})

const formatPrice = (price: number) => {
  return price.toLocaleString('vi-VN')
}
</script>

<template>
  <div class="product-card" v-if="product">
    <div class="product-image">
      <img :src="product.image" :alt="product.title" />
    </div>
    
    <div class="product-info">
      <h3 class="product-title">{{ product.title }}</h3>
      <p v-if="product.variant" class="product-variant">{{ product.variant }}</p>
      
      <div class="product-pricing">
        <div class="price-section">
          <span v-if="product.originalPrice" class="original-price">
            ₫{{ formatPrice(product.originalPrice) }}
          </span>
          <span class="current-price">₫{{ formatPrice(product.price) }}</span>
        </div>
        <div class="quantity">
          <span>x{{ product.quantity }}</span>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.product-card {
  display: flex;
  padding: 20px;
  border-bottom: 1px solid #f0f0f0;
  gap: 16px;
}

.product-card:last-child {
  border-bottom: none;
}

.product-image {
  flex-shrink: 0;
  width: 80px;
  height: 80px;
  border-radius: 8px;
  overflow: hidden;
  border: 1px solid #f0f0f0;
}

.product-image img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  transition: transform 0.2s;
}

.product-image:hover img {
  transform: scale(1.05);
}

.product-info {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.product-title {
  font-size: 14px;
  font-weight: 400;
  color: #333;
  line-height: 1.4;
  margin: 0;
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

.product-variant {
  font-size: 12px;
  color: #666;
  margin: 0;
}

.product-pricing {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: auto;
}

.price-section {
  display: flex;
  align-items: center;
  gap: 8px;
}

.original-price {
  font-size: 12px;
  color: #999;
  text-decoration: line-through;
}

.current-price {
  font-size: 16px;
  font-weight: 600;
  color: var(--bs-primary);
}

.quantity {
  font-size: 14px;
  color: #666;
}

@media (max-width: 768px) {
  .product-card {
    padding: 16px;
    gap: 12px;
  }
  
  .product-image {
    width: 60px;
    height: 60px;
  }
  
  .product-title {
    font-size: 13px;
  }
  
  .current-price {
    font-size: 14px;
  }
  
  .product-pricing {
    flex-direction: column;
    align-items: flex-start;
    gap: 4px;
  }
}
</style>
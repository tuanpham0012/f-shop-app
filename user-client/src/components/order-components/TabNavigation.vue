<script setup lang="ts">
defineProps<{
  activeTab: string
}>()

defineEmits<{
  tabChange: [tab: string]
}>()

const tabs = [
  { id: 'all', label: 'Tất cả' },
  { id: 'pending-payment', label: 'Chờ thanh toán' },
  { id: 'shipping', label: 'Vận chuyển' },
  { id: 'pending-delivery', label: 'Chờ giao hàng' },
  { id: 'completed', label: 'Hoàn thành' },
  { id: 'cancelled', label: 'Đã hủy' },
  { id: 'return-refund', label: 'Trả hàng/Hoàn tiền' }
]
</script>

<template>
  <div class="tab-navigation">
    <div class="tab-container">
      <button
        v-for="tab in tabs"
        :key="tab.id"
        :class="['tab-button', { active: activeTab === tab.id }]"
        @click="$emit('tabChange', tab.id)"
      >
        {{ tab.label }}
      </button>
    </div>
  </div>
</template>

<style scoped>
.tab-navigation {
  background: white;
  border-bottom: 1px solid #e0e0e0;
  padding: 0 20px;
}

.tab-container {
  display: flex;
  overflow-x: auto;
  -webkit-overflow-scrolling: touch;
  scrollbar-width: none;
  -ms-overflow-style: none;
}

.tab-container::-webkit-scrollbar {
  display: none;
}

.tab-button {
  background: none;
  border: none;
  padding: 16px 20px;
  cursor: pointer;
  font-size: 14px;
  font-weight: 500;
  color: #666;
  border-bottom: 3px solid transparent;
  transition: all 0.2s;
  white-space: nowrap;
  flex-shrink: 0;
}

.tab-button:hover {
  color: var(--bs-primary);
}

.tab-button.active {
  color: var(--bs-primary);
  border-bottom-color: var(--bs-primary);
}

@media (max-width: 768px) {
  .tab-navigation {
    padding: 0 12px;
  }
  
  .tab-button {
    padding: 12px 16px;
    font-size: 13px;
  }
}
</style>
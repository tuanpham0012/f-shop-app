<template>
  <div class="rating-overview">
    <div class="rating-summary">
      <div class="rating-score">
        <span class="score">5.0</span>
        <span class="total">trên 5</span>
      </div>
      <div class="stars">
        <span v-for="n in 5" :key="n" class="star">★</span>
      </div>
    </div>

    <div class="rating-filters">
      <div class="filter-tabs">
        <button 
          v-for="tab in filterTabs" 
          :key="tab.id"
          :class="['tab', { active: activeTab === tab.id }]"
          @click="activeTab = tab.id"
        >
          {{ tab.label }}
        </button>
      </div>
      
      <div class="content-filters">
        <button 
          v-for="filter in contentFilters" 
          :key="filter.id"
          :class="['content-filter', { active: activeContentFilter === filter.id }]"
          @click="activeContentFilter = filter.id"
        >
          {{ filter.label }}
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const activeTab = ref('all')
const activeContentFilter = ref('comment')

const filterTabs = [
  { id: 'all', label: 'Tất Cả' },
  { id: '5', label: '5 Sao (48,4k)' },
  { id: '4', label: '4 Sao (890)' },
  { id: '3', label: '3 Sao (149)' },
  { id: '2', label: '2 Sao (30)' },
  { id: '1', label: '1 Sao (26)' }
]

const contentFilters = [
  { id: 'comment', label: 'Có Bình Luận (29,1k)' },
  { id: 'media', label: 'Có Hình Ảnh / Video (19,7k)' }
]
</script>

<style scoped>
.rating-overview {
  padding: 20px 24px;
  border-bottom: 1px solid #f0f0f0;
}

.rating-summary {
  display: flex;
  align-items: center;
  gap: 16px;
  margin-bottom: 20px;
}

.rating-score {
  display: flex;
  align-items: baseline;
  gap: 8px;
}

.score {
  font-size: 32px;
  font-weight: bold;
  color: var(--bs-primary);
}

.total {
  font-size: 14px;
  color: #666;
}

.stars {
  display: flex;
  gap: 2px;
}

.star {
  color: var(--bs-primary);
  font-size: 20px;
}

.filter-tabs {
  display: flex;
  gap: 8px;
  margin-bottom: 12px;
  flex-wrap: wrap;
}

.tab {
  padding: 8px 16px;
  border: 1px solid #d9d9d9;
  background: white;
  color: #666;
  border-radius: 6px;
  cursor: pointer;
  font-size: 14px;
  transition: all 0.2s;
}

.tab:hover {
  border-color: var(--bs-primary);
  color: var(--bs-primary);
}

.tab.active {
  background: var(--bs-primary);
  color: white;
  border-color: var(--bs-primary);
}

.content-filters {
  display: flex;
  gap: 8px;
  flex-wrap: wrap;
}

.content-filter {
  padding: 6px 12px;
  border: none;
  background: #f5f5f5;
  color: #666;
  border-radius: 4px;
  cursor: pointer;
  font-size: 13px;
  transition: all 0.2s;
}

.content-filter:hover {
  background: #e6f7ff;
  color: #1890ff;
}

.content-filter.active {
  background: #1890ff;
  color: white;
}
</style>
<template>
  <div class="review-item">
    <div class="review-header">
      <div class="user-info">
        <div class="avatar">
          <img :src="review.avatar" :alt="review.userName" />
        </div>
        <div class="user-details">
          <div class="user-name">{{ review.userName }}</div>
          <div class="rating-stars">
            <span v-for="n in 5" :key="n" :class="['star', { filled: n <= review.rating }]">★</span>
          </div>
        </div>
      </div>
    </div>

    <div class="review-meta">
      <span class="date">{{ review.date }}</span>
      <span class="separator">|</span>
      <span class="product-info">{{ review.productInfo }}</span>
    </div>

    <div class="review-content">
      {{ review.content }}
    </div>

    <div v-if="review.images.length" class="review-images">
      <div v-for="(image, index) in review.images" :key="index" class="image-container">
        <img :src="image" :alt="`Review image ${index + 1}`" />
        <div v-if="index === 0 && hasVideo" class="video-indicator">
          <span class="play-icon">▶</span>
          <span class="duration">0:13</span>
        </div>
      </div>
    </div>

    <div class="review-actions">
      <button class="like-button" @click="toggleLike">
        <span class="like-icon">👍</span>
        <span class="like-count">{{ review.likes }}</span>
      </button>
    </div>

    <div v-if="review.sellerResponse" class="seller-response">
      <div class="response-header">{{ review.sellerResponse.title }}</div>
      <div class="response-content">{{ review.sellerResponse.content }}</div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'

interface Review {
  id: number
  userName: string
  avatar: string
  rating: number
  date: string
  productInfo: string
  content: string
  images: string[]
  likes: number
  sellerResponse?: {
    title: string
    content: string
  }
}

const props = defineProps<{
  review: Review
}>()

const liked = ref(false)
const hasVideo = computed(() => props.review.id === 1) // Mock video for first review

const toggleLike = () => {
  liked.value = !liked.value
  if (liked.value) {
    props.review.likes++
  } else {
    props.review.likes--
  }
}
</script>

<style scoped>
.review-item {
  padding: 20px 24px;
  border-bottom: 1px solid #f0f0f0;
}

.review-item:last-child {
  border-bottom: none;
}

.review-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 8px;
}

.user-info {
  display: flex;
  align-items: center;
  gap: 12px;
}

.avatar {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  overflow: hidden;
  background-color: #f0f0f0;
}

.avatar img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.user-name {
  font-weight: 500;
  color: #333;
  font-size: 14px;
  margin-bottom: 4px;
}

.rating-stars {
  display: flex;
  gap: 1px;
}

.star {
  font-size: 14px;
  color: #d9d9d9;
}

.star.filled {
  color: var(--bs-primary);
}

.review-meta {
  font-size: 12px;
  color: #999;
  margin-bottom: 12px;
}

.separator {
  margin: 0 8px;
}

.review-content {
  font-size: 14px;
  line-height: 1.6;
  color: #333;
  margin-bottom: 16px;
}

.review-images {
  display: flex;
  gap: 8px;
  margin-bottom: 16px;
  flex-wrap: wrap;
}

.image-container {
  width: 80px;
  height: 80px;
  border-radius: 6px;
  overflow: hidden;
  position: relative;
  background-color: #f0f0f0;
}

.image-container img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  cursor: pointer;
  transition: transform 0.2s;
}

.image-container img:hover {
  transform: scale(1.05);
}

.video-indicator {
  position: absolute;
  bottom: 4px;
  right: 4px;
  background: rgba(0, 0, 0, 0.7);
  color: white;
  padding: 2px 6px;
  border-radius: 4px;
  font-size: 10px;
  display: flex;
  align-items: center;
  gap: 4px;
}

.play-icon {
  font-size: 8px;
}

.review-actions {
  margin-bottom: 16px;
}

.like-button {
  display: flex;
  align-items: center;
  gap: 6px;
  background: none;
  border: none;
  color: #999;
  cursor: pointer;
  font-size: 12px;
  padding: 4px 0;
  transition: color 0.2s;
}

.like-button:hover {
  color: #1890ff;
}

.like-icon {
  font-size: 14px;
}

.seller-response {
  background-color: #f9f9f9;
  padding: 12px;
  border-radius: 6px;
  border-left: 3px solid #1890ff;
}

.response-header {
  font-weight: 500;
  color: #1890ff;
  font-size: 13px;
  margin-bottom: 8px;
}

.response-content {
  font-size: 13px;
  line-height: 1.6;
  color: #555;
}
</style>
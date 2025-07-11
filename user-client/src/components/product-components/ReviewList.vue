<template>
  <div class="review-list">
    <ReviewItem 
      v-for="review in reviews" 
      :key="review.id"
      :review="review"
    />
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import ReviewItem from './ReviewItem.vue'

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

const reviews = ref<Review[]>([
  {
    id: 1,
    userName: 'l****u',
    avatar: 'https://images.pexels.com/photos/1337753/pexels-photo-1337753.jpeg?auto=compress&cs=tinysrgb&w=50&h=50&dpr=1',
    rating: 5,
    date: '2021-10-02 12:34',
    productInfo: 'Phân loại hàng: Đơ 700 +TẶNG 50G T12',
    content: 'Ngũ cốc giòn ăn ngon ít ngán hơn các loại khác, ăn k cũng được và trộn với sữa chua lạnh lạnh ăn cũng khá ngon. Shop rất nhiệt tình đọt trước đặt do dịch hàng bị lư hoãn về mà nt shop vẫn rất vui vẻ rep. Sẽ tiếp tục ủng hộ shop!!',
    images: [
      'https://images.pexels.com/photos/1092730/pexels-photo-1092730.jpeg?auto=compress&cs=tinysrgb&w=100&h=100&dpr=1',
      'https://images.pexels.com/photos/1640777/pexels-photo-1640777.jpeg?auto=compress&cs=tinysrgb&w=100&h=100&dpr=1',
      'https://images.pexels.com/photos/1171671/pexels-photo-1171671.jpeg?auto=compress&cs=tinysrgb&w=100&h=100&dpr=1',
      'https://images.pexels.com/photos/209540/pexels-photo-209540.jpeg?auto=compress&cs=tinysrgb&w=100&h=100&dpr=1',
      'https://images.pexels.com/photos/6896379/pexels-photo-6896379.jpeg?auto=compress&cs=tinysrgb&w=100&h=100&dpr=1'
    ],
    likes: 83,
    sellerResponse: {
      title: 'Phản Hồi Của Người Bán',
      content: 'Cảm ơn khách yêu đã đánh tâng Shop đánh giá 5 sao siêu đẻ thương á. Bạn inbox để shop gửi tặng liền tay cho bạn Voucher giảm giá 20k cho lần mua hàng sau nhé! >< Việt Thái Organic luôn có khuyến mãi KHỦNG mỗi ngày cho hàng loạt sản phẩm mới. Khách nhớ bấm theo dõi Shop và ghé trang để không bỏ lỡ ưu đãi nha. Mong được sớm phục vụ bạn ở đơn hàng sau. <3'
    }
  },
  {
    id: 2,
    userName: 'emmathao',
    avatar: 'https://images.pexels.com/photos/774909/pexels-photo-774909.jpeg?auto=compress&cs=tinysrgb&w=50&h=50&dpr=1',
    rating: 5,
    date: '2022-11-17 12:03',
    productInfo: 'Phân loại hàng: Đơ 700 +TẶNG 50G T12',
    content: 'Vừa ăn ko quá béo. Giá cả lại càng hợp lý, ăn diet hết bản cả nữa rồi!! Tiếc là đến mãi giờ mới nhận ra dc ngũ cốc ngon nhất mình từng ăn. Sẽ tiếp tuc mua thêm vì mỗi mua về mà chồng mình mê đói ăn gần hết haha. Thanks shop nhé',
    images: [
      'https://images.pexels.com/photos/1092730/pexels-photo-1092730.jpeg?auto=compress&cs=tinysrgb&w=100&h=100&dpr=1',
      'https://images.pexels.com/photos/1640777/pexels-photo-1640777.jpeg?auto=compress&cs=tinysrgb&w=100&h=100&dpr=1',
      'https://images.pexels.com/photos/209540/pexels-photo-209540.jpeg?auto=compress&cs=tinysrgb&w=100&h=100&dpr=1'
    ],
    likes: 45
  }
])
</script>

<style scoped>
.review-list {
  padding: 0;
}
</style>
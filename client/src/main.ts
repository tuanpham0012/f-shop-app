import './assets/css/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

import { Cropper } from 'vue-advanced-cropper'
import 'vue-advanced-cropper/dist/style.css';

import globalComponent from '@/helpers/import-global-components';

import { QuillEditor } from '@vueup/vue-quill'
import '@vueup/vue-quill/dist/vue-quill.snow.css';

import Editor from "@tinymce/tinymce-vue";

const app = createApp(App)

Object.entries(globalComponent).forEach(([name, component]) => {
    app.component(name, component);
})

app.directive('click-outside', {
    mounted(el, binding) {
    // Lưu trữ handler trên phần tử để có thể gỡ bỏ sau này
    el.__ClickOutsideHandler__ = (event:any) => {
      // Nếu phần tử được click nằm ngoài `el` VÀ click không phải trên `el` itself
      if (!(el === event.target || el.contains(event.target))) {
        // Kiểm tra xem binding.value có phải là một hàm không
        if (typeof binding.value === 'function') {
          binding.value(event); // Gọi hàm callback được truyền vào directive
        }
      }
    };
    document.addEventListener('click', el.__ClickOutsideHandler__);
  },
  // `unmounted` (Vue 3) hoặc `unbind` (Vue 2)
  unmounted(el) {
    // Gỡ bỏ event listener khi phần tử bị hủy
    document.removeEventListener('click', el.__ClickOutsideHandler__);
    delete el.__ClickOutsideHandler__; // Xóa tham chiếu để tránh rò rỉ bộ nhớ
    }
});

app.component('Cropper', Cropper);
app.component('QuillEditor', QuillEditor)
app.component('Editor', Editor)
app.use(createPinia())
app.use(router)

app.mount('#app')

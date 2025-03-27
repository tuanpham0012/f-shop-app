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
    mounted(el, binding, vnode) {
        el.clickOutsideEvent = function (event: any) {
            if (!(el === event.target || el.contains(event.target))) {
                binding.value(event, el);
            }
        };
        document.body.addEventListener('click', el.clickOutsideEvent);
    },
    unmounted(el) {
        document.body.removeEventListener('click', el.clickOutsideEvent);
    }
});

app.component('Cropper', Cropper);
app.component('QuillEditor', QuillEditor)
app.component('Editor', Editor)
app.use(createPinia())
app.use(router)

app.mount('#app')

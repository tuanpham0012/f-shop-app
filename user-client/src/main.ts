import './assets/css/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

import { Cropper } from 'vue-advanced-cropper'
import 'vue-advanced-cropper/dist/style.css';

import globalComponent from '@/helpers/import-global-components';

import vue3GoogleLogin from 'vue3-google-login';

// import { QuillEditor } from '@vueup/vue-quill'
// import '@vueup/vue-quill/dist/vue-quill.snow.css';

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
app.use(vue3GoogleLogin , {
  clientId: `${import.meta.env.VITE_VUE_APP_GG_CLIENT_ID}`,
});
// app.component('QuillEditor', QuillEditor)
app.use(createPinia())
app.use(router)

app.mount('#app')

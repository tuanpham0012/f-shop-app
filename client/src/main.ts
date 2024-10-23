import './assets/css/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

import globalComponent from '@/helpers/import-global-components';

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

app.use(createPinia())
app.use(router)

app.mount('#app')

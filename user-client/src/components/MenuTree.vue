<template lang="">
    <li
        class="menu-item"
        v-if="menu"
        :class="{ 'open ss active': checkRouteActive(menu) }"
    >
        <component
            :is="(menu.children.length > 0 || menu.url == null) ? 'div' : 'router-link'"
            :to="(menu.children.length > 0 || menu.url == null) ? '' : menu.url"
            class="menu-link"
            :class="{ 'menu-toggle': menu.children.length > 0 }"
        >
            <div
                class="w-[20px] h-[20px] me-2"
                v-if="menu.icon"
                v-html="menu.icon"
            ></div>
            <div>{{ menu.title }}</div>
        </component>
        <ul class="menu-sub" v-if="menu.children.length > 0">
            <MenuTree
                v-for="(item, index) in menu.children"
                :key="index"
                :menu="item"
            >
            </MenuTree>
        </ul>
    </li>
</template>
<script lang="ts" setup>
import { watch, ref } from "vue";
import { useRoute } from "vue-router";
const props = defineProps({
    menu: {
        type: Object,
        default: function () {
            return {
                url: "",
                children: [],
            };
        },
    },
});

const route = useRoute();
const path = ref("");

const checkRouteActive = (item: any) => {
    if (!item) return false;
    if (item.url == path.value) return true;

    for (var i = 0; i < item.children.length; i++) {
        if (checkRouteActive(item.children[i])) {
            return true;
        }
    }
    return false;
};

watch(
    route,
    (to) => {
        path.value = to.path;
    },
    { flush: "pre", immediate: true, deep: true }
);
</script>
<style lang=""></style>

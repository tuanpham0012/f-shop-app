<template lang="">
    <!-- <div class="description" id="reset-this-root" v-html="description"></div> -->
    <div class="relative" v-if="description">
        <iframe
            v-if="description"
            :srcdoc="description"
            width="100%"
            height="600"
            frameborder="0"
            ref="iframe"
            @load="setIframeHeight"
        ></iframe>
        <div
            class="show more flex justify-center items-center"
            v-if="!showMore"
        >
            <a class="text-md cursor-pointer" @click="toggleShowMore">
                Xem thêm
            </a>
        </div>
        <div
            class="show less flex justify-center items-center"
            v-if="showMore"
        >
            <a class="text-md cursor-pointer" @click="toggleShowMore">
                Rút gọn
            </a>
        </div>
    </div>
</template>
<script setup lang="ts">
import { ref, computed, onBeforeMount, watch } from "vue";
import { useProductStore } from "../../stores/product";
const productStore = useProductStore();

const iframe = ref<any>(null);

const showMore = ref(false);

const description = computed(() => productStore.$state.descriptionProduct.data ?? "");

const setIframeHeight = () => {
    let count = 0;
    let intervalId = setInterval(() => {
        if (iframe.value) {
            //   const contentHeight =
            //     iframe.value.contentWindow.document.body.scrollHeight + 50;
            //   iframe.value.style.height = contentHeight + "px";
            iframe.value.contentWindow.document.body.style.overflow = "hidden";
        }
        count++;
        if (count > 10) clearInterval(intervalId);
    }, 200);
    const doc = iframe.value.contentWindow.document;
    if (doc) {
        const link1 = document.createElement("link");
        link1.rel = "stylesheet";
        link1.type = "text/css";
        link1.href = "/src/assets/css/editor/content.css";
        doc.head.appendChild(link1);
        const link2 = document.createElement("link");
        link2.rel = "stylesheet";
        link2.type = "text/css";
        link2.href = "/src/assets/css/editor/content.min.css";
        doc.head.appendChild(link2);
        const link3 = document.createElement("script");
        link3.src = "/src/assets/css/editor/content.js";
        doc.body.appendChild(link3);
    }
};

const toggleShowMore = () => {
    showMore.value = !showMore.value;
    if (showMore.value) {
        const contentHeight =
            iframe.value.contentWindow.document.body.scrollHeight + 50;
        iframe.value.style.height = contentHeight + "px";
    } else {
        iframe.value.style.height = "600px";
    }
};

onBeforeMount(async () => {
    // if(props.alias != '')
    // await productStore.getDescriptionProductByAlias(props.alias);
});
</script>
<style lang="scss" scoped>
.show {
    &.more {
        position: absolute;
        bottom: 0;
        width: 100%;
        height: 4rem;
        background: rgb(195, 195, 195);
        background: linear-gradient(
            0deg,
            rgba(195, 195, 195, 0.9051995798319328) 0%,
            rgba(217, 217, 217, 0.6222864145658263) 30%,
            rgba(245, 245, 245, 0.14049369747899154) 72%,
            rgba(255, 255, 255, 0.04245448179271705) 87%,
            rgba(255, 255, 255, 0) 100%
        );
    }
    a {
        background-color: rgb(248, 248, 248);
        padding: .5rem 1rem;
        color: black;
        border: 1px solid rgb(192, 192, 192);
        border-radius: 3rem;
    }
}
</style>

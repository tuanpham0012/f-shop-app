<template>
    <div class="container-lg" v-if="product">
        <nav aria-label="breadcrumb" class="hidden lg:block breadcrumb-nav">
            <div class="container-md">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="index.html">Sản phẩm</a></li>
                    <li class="breadcrumb-item">
                        <a href="#">{{ product.category?.name }}</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">
                        {{ product.name }}
                    </li>
                </ol>
            </div>
            <!-- End .container -->
        </nav>
        <div class="block sm:hidden">
            <button class="btn btn-icon">
                <i class="icon-long-arrow-left text-2xl"></i>
            </button>
        </div>
        <div class="grid grid-cols-12 gap-0 mb-4 mt-1">
            <div class="col-span-12 lg:col-span-6 bg-white h-100 px-2">
                <div class="rounded-3 d-flex justify-content-center w-[100] h-[auto] aspect-square d-flex justify-center items-center p-4 box-border" v-show="imgSelect">
                    <a data-fslightbox="mygalley" class="rounded-4" target="_blank" data-type="image">
                        <img class="w-100 h-100 object-contain" loading="lazy" :src="imgSelect" />
                    </a>
                </div>
                <swiper
                    :style="{
                        '--swiper-navigation-color': '#fff',
                        '--swiper-pagination-color': '#fff',
                    }"
                    :spaceBetween="10"
                    :navigation="false"
                    :centeredSlides="true"
                    :thumbs="{ swiper: thumbsSwiper }"
                    :modules="modules"
                    class="mySwiper2"
                    v-show="!imgSelect"
                >
                    <swiper-slide v-for="(img, index) in product.images"
                        ><div class="rounded-3 d-flex justify-content-center w-[100] h-[auto] aspect-square d-flex justify-center items-center p-4 box-border">
                            <a data-fslightbox="mygalley" class="rounded-4" target="_blank" data-type="image">
                                <img class="w-100 h-100 object-contain" loading="lazy" :src="img.path" />
                            </a></div
                    ></swiper-slide>
                </swiper>
                <swiper
                    @swiper="setThumbsSwiper"
                    :freeMode="true"
                    :watchSlidesProgress="true"
                    :modules="modules"
                    :breakpoints="{
                        '0': {
                            slidesPerView: 5,
                            spaceBetween: 5,
                        },
                        '576': {
                            slidesPerView: 6,
                            spaceBetween: 5,
                        },
                        '768': {
                            slidesPerView: 7,
                            spaceBetween: 5,
                        },
                    }"
                    class="mySwiper my-3"
                >
                    <swiper-slide v-for="(img, index) in product.images" :key="index" @click="imgSelect = null">
                        <img class="w-[75px] h-[75px] object-contain border p-1 rounded-sm cursor-pointer" loading="lazy" :src="img.path" />
                    </swiper-slide>
                </swiper>
            </div>
            <div class="col-span-12 lg:col-span-6 bg-white px-4 py-3 lg:pt-5 grid">
                <div>
                    <h4 class="title text-dark">
                        {{ product.name }}
                    </h4>
                    <div class="flex flex-row my-3 justify-start text-base">
                        <div class="flex justify-center items-center text-warning me-2 border-r pe-2">
                            <i class="fa fa-star"></i>
                            <span class="ms-1"> 4.5 </span>
                        </div>
                        <span class="flex justify-center items-center gap-1 text-muted border-r me-2 pe-2"><i class="fas fa-shopping-basket fa-sm mx-1"></i>154 <span class="hidden sm:block">đã bán</span></span>
                        <span class="flex justify-center items-center gap-1 text-muted border-r me-2 pe-2"><i class="fa-solid fa-comment-dots fa-sm mx-1"></i><span class="hidden sm:block">đánh giá</span>154</span>
                        <span class="flex justify-center items-center text-success" v-if="!product.soldOut">Còn hàng</span>
                        <span class="flex justify-center items-center text-danger" v-else>Hết hàng</span>
                    </div>
                    <div class="mb-2 bg-gray-100 flex gap-3 p-3 rounded-sm">
                        <span class="text-xl text-primary font-medium">{{ displayPrice(skuSelect?.price ?? product.price) + "đ" }}</span>
                        <span class="text-base text-gray-400 font-normal line-through">{{ displayPrice(skuSelect?.price ?? product.price) + "đ" }}</span>
                    </div>

                    <div class="row mt-2 mb-2 text-base">
                        <div class="grid grid-cols-12 mb-2 items-start" v-for="(option, i) in options" :key="i">
                            <div class="col-span-3">
                                <label class="py-2 font-medium">{{ option.name }}:</label>
                            </div>
                            <div class="flex gap-x-1 gap-y-2 flex-wrap col-span-9">
                                <div
                                    class="p-[4px] border rounded-md cursor-pointer text-gray-800 font-semibold"
                                    v-for="(value, j) in option.optionValues"
                                    :key="j"
                                    :class="{
                    'border-primary text-primary shadow-2xl shadow-cyan-800/30':
                      selected.findIndex((x:any) => x.optionValueId == value.id) >=
                      0,
                  }"
                                    @click="selectOption(value)"
                                >
                                    <div v-if="option.visual == 1" :class="`w-[25px] h-[25px]`" :style="`background-color: ${value.value}`"></div>
                                    <div v-else>
                                        <span class="p-2 font-normal">
                                            {{ value.label }}
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="grid grid-cols-12 mt-2">
                            <div class="col-span-3">
                                <label for="qty">Số lượng:</label>
                            </div>
                            <div class="col-span-9 product-details-quantity">
                                <div class="relative">
                                    <div class="absolute left-0 top-0">
                                        <button style="min-width: 26px" class="btn btn-decrement btn-spinner" type="button" @click="quantity--" :disabled="quantity <= 1">
                                            <i class="icon-minus text-base"></i>
                                        </button>
                                    </div>
                                    <input type="text" style="text-align: center; font-size: 1rem; height: 35px" class="form-control" v-model="quantity" />
                                    <div class="absolute right-0 top-0">
                                        <button style="min-width: 26px" class="btn btn-increment btn-spinner z-20" type="button" @click="quantity++">
                                            <i class="icon-plus text-base"></i>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="grid grid-cols-12 gap-3 py-3 mt-auto mb-3 mt-3 lg:mb-12 px-4">
                    <button class="cart-button col-span-5" ref="addToCartBtn" @click="addToCart()">
                        <span class="add-to-cart"><i class="inline-block lg:hidden fa-solid fa-cart-plus"></i> <span class="hidden lg:inline-block">Thêm vào giỏ hàng</span></span>
                        <span class="added">Đã thêm</span>
                        <i class="fas fa-shopping-cart"></i>
                        <i class="fas fa-box"></i>
                    </button>
                    <button class="btn btn-icon btn-outline-danger py-2 col-span-5">
                        <span class="text-base font-medium">Mua ngay</span>
                    </button>
                    <button class="btn btn-icon btn-outline-success py-2 col-span-2">
                        <span class="text-base font-medium"><i class="fa-solid fa-heart-circle-plus"></i></span>
                    </button>
                </div>
            </div>
            <div class="container-lg col-span-12 mt-4 bg-white mb-6">
                <div class="p-5">
                    <ProductDescription :alias="$route.params.productCode" />
                </div>
            </div>
            <div class="container-lg col-span-12 mt-4 bg-white mb-6">
                <div class="p-5">
                    <h3 class="page-title">ĐÁNH GIÁ SẢN PHẨM</h3>
                    <RatingOverview />
                    <ReviewList />
                </div>
            </div>
        </div>
    </div>
    <div class="w-[100%] h-[80vh]" v-else>
        <LoadingComponent />
    </div>
</template>
<script lang="ts" setup>
import { reactive, ref, onBeforeMount, computed, watch } from "vue";
import { useProductStore } from "@/stores/product";
import { useRoute, useRouter } from "vue-router";
import { errorMessage, warningMessage, successMessage } from "@/helpers/toast";
import { isNumber } from "@/helpers/helpers";
import { displayPrice } from "@/services/utils";
import { Swiper, SwiperSlide } from "swiper/vue";
import "swiper/css";
import "swiper/css/free-mode";
import "swiper/css/navigation";
import "swiper/css/thumbs";
import { FreeMode, Navigation, Thumbs } from "swiper/modules";
import ProductDescription from "./ProductDescription.vue";
import LoadingComponent from "@/components/LoadingComponent.vue";
import { useCartStore } from "@/stores/cart";
import RatingOverview from "@/components/product-components/RatingOverview.vue";
import ReviewList from "@/components/product-components/ReviewList.vue";

const route = useRoute();
const productStore = useProductStore();
const cartStore = useCartStore();

const modules = ref([FreeMode, Navigation, Thumbs]);
const thumbsSwiper = ref(null);
const selected = ref<any>([]);
const skuSelect = ref<any>(null);
const imgSelect = ref<any>(null);
const quantity = ref(1);
const tabViewIndex = ref(1);
const addToCartBtn = ref<any>(null);
const tabViews = reactive([
    {
        id: 1,
        label: "Thông tin sản phẩm",
        icon: "fa-solid fa-circle-info",
    },
    {
        id: 2,
        label: "ĐÁNH GIÁ SẢN PHẨM",
        icon: "fa-solid fa-comment-dots",
    },
]);

const addToCart = async () => {
    if (skuSelect.value == null) {
        warningMessage("Vui lòng chọn sản phẩm trước khi thêm vào giỏ hàng");
        return;
    }
    addToCartBtn.value.classList.add("clicked");
    setTimeout(() => {
        addToCartBtn.value.classList.remove("clicked");
    }, 2000);
    await cartStore
        .addToCart({
            skuId: skuSelect.value.id,
            quantity: quantity.value,
        })
        .then((res: any) => {
            successMessage("Thêm vào giỏ hàng thành công");
        })
        .catch((err: any) => {
            errorMessage("Thêm vào giỏ hàng thất bại");
        });
};

const setThumbsSwiper = (swiper: any) => {
    thumbsSwiper.value = swiper;
};

const selectOption = (value: any) => {
    skuSelect.value = null;
    const item = {
        optionId: value.optionId,
        optionValueId: value.id,
    };
    //check item selected
    const index = selected.value.findIndex((x: any) => x.optionId == value.optionId);
    let oldValue = null;
    if (index >= 0) {
        oldValue = selected.value[index].optionValueId;
        selected.value.splice(index, 1);
        if (oldValue == item.optionValueId) return;
    }
    selected.value.push(item);
    skus.value.every((sku: any) => {
        const check = onlyInLeft(sku.variants, selected.value, isSameUser);
        if (check.length == 0) {
            skuSelect.value = sku;
            return false;
        }
        return true;
    });
};

const isSameUser = (a: any, b: any) => a.optionId === b.optionId && a.optionValueId === b.optionValueId;

const onlyInLeft = (left: any, right: any, compareFunction: any) => left.filter((leftValue: any) => !right.some((rightValue: any) => compareFunction(leftValue, rightValue)));

const product = computed<any>(() => productStore.$state.product);
const options = computed<any>(() => productStore.$state.options);
const skus = computed<any>(() => productStore.$state.skus);

watch(
    () => quantity.value,
    (newValue) => {
        if (newValue < 1) quantity.value = 1;
    }
);

watch(
    () => skus.value,
    (newValue) => {
        if (skus.value.length == 1) skuSelect.value = skus.value[0];
        console.log(skus.value[0]);
    },
    { deep: true }
);

watch(
    () => skuSelect.value,
    (newValue) => {
        if (newValue) {
            imgSelect.value = newValue.imagePath;
        }
    },
    { deep: true }
);

onBeforeMount(async () => {
    if (route.params.productCode != null) await productStore.getProductByAlias(route.params.productCode);
});
</script>
<style lang="scss">
.swiper-button-prev:after,
.swiper-button-next:after {
    font-family: "swiper-icons";
    font-size: 3rem;
    font-weight: 500;
}
li {
    .tab-view {
        color: var(--bs-secondary);
        &:hover {
            border-bottom-color: var(--bs-info);
            color: var(--bs-info);
        }
        &.active {
            border-bottom-color: var(--bs-primary);
            color: var(--bs-primary);
            pointer-events: none;
        }
    }
}

.cart-button {
    position: relative;
    padding: 10px;
    width: 100%;
    height: 45px;
    border: 0;
    border-radius: 6px;
    border: 1px solid var(--bs-primary);
    outline: none;
    font-size: 1rem;
    cursor: pointer;
    color: var(--bs-primary) !important;
    transition: 0.3s ease-in-out;
    overflow: hidden;
    .add-to-cart {
        width: 100%;
        color: var(--bs-primary);
    }
}
.cart-button:hover {
    background-color: var(--bs-primary);
    .add-to-cart {
        color: var(--bs-text-white);
    }
    i {
        color: var(--bs-text-white);
    }
}
.cart-button:active {
    transform: scale(0.9);
    i {
        color: var(--bs-text-white);
    }
}

.cart-button .fa-shopping-cart {
    position: absolute;
    z-index: 2;
    top: 50%;
    left: -15%;
    font-size: 1.2rem;
    transform: translate(-50%, -50%);
}
.cart-button .fa-box {
    position: absolute;
    z-index: 3;
    top: -20%;
    left: 52%;
    font-size: 0.875rem;
    transform: translate(-50%, -50%);
}
.cart-button span {
    position: absolute;
    z-index: 3;
    left: 50%;
    top: 50%;
    font-size: 1rem;
    font-weight: 500;
    width: 100%;
    // color: #fff;
    transform: translate(-50%, -50%);
}
.cart-button span.add-to-cart {
    opacity: 1;
}
.cart-button span.added {
    opacity: 0;
}

.cart-button.clicked .fa-shopping-cart {
    animation: cart 1.5s ease-in-out forwards;
}
.cart-button.clicked .fa-box {
    animation: box 1.5s ease-in-out forwards;
}
.cart-button.clicked span.add-to-cart {
    animation: txt1 1.5s ease-in-out forwards;
}
.cart-button.clicked span.added {
    animation: txt2 1.5s ease-in-out forwards;
}
@keyframes cart {
    0% {
        left: -10%;
    }
    40%,
    60% {
        left: 50%;
    }
    100% {
        left: 110%;
    }
}
@keyframes box {
    0%,
    40% {
        top: -20%;
    }
    60% {
        top: 40%;
        left: 52%;
    }
    100% {
        top: 40%;
        left: 112%;
    }
}
@keyframes txt1 {
    0% {
        opacity: 1;
    }
    20%,
    100% {
        opacity: 0;
    }
}
@keyframes txt2 {
    0%,
    80% {
        opacity: 0;
    }
    100% {
        opacity: 1;
    }
}

.swiper-button-prev,
.swiper-rtl .swiper-button-next {
    left: var(--swiper-navigation-sides-offset, -10px);
    right: auto;
}
.swiper-button-next,
.swiper-rtl .swiper-button-prev {
    right: var(--swiper-navigation-sides-offset, -10px);
    left: auto;
}
</style>

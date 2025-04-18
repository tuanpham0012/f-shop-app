<template>
    <div v-click-outside="() => toggleShow(false)" class="box">
        <div
            class="select-box"
            :class="{ active: showList, disabled: props.disabled }"
            ref="container"
        >
            <div
                class="selected"
                :id="props.idElement"
                @click="toggleShow()"
                :class="{ focus: showList == true }"
            >
                <span class="content" style="white-space: nowrap">{{
                    dataSelect != null && props.display in dataSelect
                        ? dataSelect[props.display]
                        : props.placeholder
                }}</span>
                <span class="icon" v-if="!disabled"
                    ><i class="fa-solid fa-chevron-right fs-8 text-gray-600"></i
                ></span>
            </div>

            <div
                class="options-container"
                v-if="showList"
                :style="{
                    top: top + 'px',
                    left: left + 'px',
                    minWidth: maxWidth + 'px',
                }"
            >
                <div class="search-box">
                    <input
                        type="text"
                        placeholder="Tìm kiếm..."
                        v-model="searchData"
                    />
                </div>
                <div class="list">
                    <div
                        class="option"
                        :class="{
                            active: props.modelValue == null,
                        }"
                        id="1"
                        @click="setDataSelect(null)"
                        v-if="
                            listData &&
                            listData.length > 0 &&
                            !props.firstSelected
                        "
                    >
                        {{ props.placeholder }}
                    </div>
                    <div
                        class="option"
                        v-for="(item, index) in listData"
                        :id="
                            dataSelect &&
                            item[props.keyValue] == dataSelect[props.keyValue]
                                ? idElementList
                                : null
                        "
                        :key="index"
                        :class="{
                            active:
                                dataSelect &&
                                item[props.keyValue] ==
                                    dataSelect[props.keyValue],
                        }"
                        @click="setDataSelect(item)"
                    >
                        <span>{{ item[props.display] }}</span>
                    </div>
                    <div class="option disabled" v-if="listData.length == 0">
                        Không có dữ liệu
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script setup>
import { ref, watch, computed, onMounted, onBeforeMount, reactive } from "vue";
import debounce from "lodash.debounce";
import { removeVietnameseTones } from "@/services/utils";
import { storeToRefs } from "pinia";

const props = defineProps({
    firstSelected: {
        type: Boolean,
        default: false,
    },
    placeholder: {
        type: String,
        default: "--Tất cả--",
    },
    keyValue: {
        type: String,
        default: "id",
    },
    display: {
        type: String,
        default: "id",
    },
    listData: {
        type: Array,
        required: true,
        default: function () {
            return [];
        },
    },
    modelValue: {
        type: [String, Number],
        default: null,
    },
    disabled: {
        type: Boolean,
        default: false,
    },
    idElement: {
        type: [String, Number],
        default: function () {
            let result = "";
            const characters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const charactersLength = characters.length;
            let counter = 0;
            while (counter < 24) {
                result += characters.charAt(
                    Math.floor(Math.random() * charactersLength)
                );
                counter += 1;
            }
            return result;
        },
    },
    idElementList: {
        type: [String, Number],
        default: function () {
            let result = "";
            const characters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const charactersLength = characters.length;
            let counter = 0;
            while (counter < 24) {
                result += characters.charAt(
                    Math.floor(Math.random() * charactersLength)
                );
                counter += 1;
            }
            return result;
        },
    },
});

const emits = defineEmits([
    "search-data",
    "update:modelValue",
    "changeDataSelect",
]);

// const dataSelect = ref(props.listData.find(x => x[props.keyValue] == props.modelValue));

const dataSelect = ref(
    props.listData.find((x) => x[props.keyValue] == props.modelValue)
);

const searchData = ref("");

const listData = computed(() => {
    return props.listData.filter((e) =>
        removeVietnameseTones(e[props.display])
            .toLowerCase()
            .includes(removeVietnameseTones(searchData.value.toLowerCase()))
    );
});

const showList = ref(false);

const top = ref(0);
const left = ref(0);

const maxWidth = ref(100);
const minWidth = ref(200);

watch(
    () => props.listData,
    (newValue, oldValue) => {
        dataSelect.value = newValue.find(
            (x) => x[props.keyValue] == props.modelValue
        );
    }
);

watch(
    () => props.modelValue,
    (newValue, oldValue) => {
        // console.log(props.listData.find(x => x[props.keyValue] == newValue));
        dataSelect.value = props.listData.find(
            (x) => x[props.keyValue] == newValue
        );
    }
);

watch(
    searchData,
    debounce((newValue, oldValue) => {
        emits("search-data", newValue);
    }, 50)
);

watch(dataSelect, (newValue, oldValue) => {
    emits(
        "update:modelValue",
        newValue == null ? null : newValue[props.keyValue]
    );
});

const toggleShow = (state = null) => {
    if (state === null) {
        showList.value = !showList.value;
    } else {
        showList.value = state;
    }
    setTimeout(function () {
        const e = document.getElementById(props.idElementList);
        if (e) {
            e.scrollIntoView();
        }
    }, 10);
    // position();
};

const setDataSelect = (data) => {
    showList.value = !showList.value;
    if (data != null) {
        dataSelect.value = data;
    } else {
        dataSelect.value = null;
    }
    emits("changeDataSelect", data);
};

const position = () => {
    let elem = document.getElementById(props.idElement);
    if (elem) {
        let rect = elem.getBoundingClientRect();

        if (rect.top + 350 + elem.offsetHeight > window.screen.height) {
            top.value = rect.top - 295;
        } else {
            top.value = rect.top + elem.offsetHeight;
        }
        left.value = rect.left;
        maxWidth.value = elem.offsetWidth;
        minWidth.value = elem.offsetWidth;
        showList.value = false;
    }
};

defineExpose({
    toggleShow,
});

onMounted(() => {
    var divs = document.getElementsByTagName("div");
    for (var i = 0; i < divs.length; i++) {
        divs[i].addEventListener("scroll", position);
        new ResizeObserver(position).observe(divs[i]);
    }
    position();
});
</script>
<style lang="scss" scoped>
.select-box {
    position: relative;
    width: auto;
    flex-direction: column;

    .options-container {
        position: fixed;
        background: rgb(250, 249, 249);
        width: max-content;
        max-width: 100%;
        opacity: 1;
        border-radius: 4px;
        overflow: hidden;
        box-sizing: content-box;
        order: 1;
        color: rgb(43, 41, 41);
        z-index: 100;
        border: 1px solid #d3d3d3;
        .list {
            max-height: 250px;
            overflow: auto;
            width: 100%;
            overflow-x: hidden;
            box-shadow: 0px 10px 70px 4px rgba(0, 0, 0, 0.13);
            padding: 0.4rem 0 0 0.4rem;
        }
    }
}

.select-box .option {
    padding: 6px 8px;
    cursor: pointer;
    text-align: left;
    font-size: 13px;
}

.selected {
    display: flex;
    align-items: center;
    cursor: pointer;
    padding: 5px 14px;
    width: 100%;
    // max-height: 37.5px;
    border: 1px solid #d9dee3;
    border-radius: 0.4rem;
    margin-bottom: 1px;
    color: #697a8d;
    position: absolute;
    order: 0;
    background-color: #ffffff;
    box-shadow: 0px 10px 70px 4px rgba(180, 180, 180, 0.13);
    transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;

    .content {
        width: 100% !important;
        height: 28px;
        line-height: 28px;
        overflow: hidden;
        text-overflow: ellipsis;
        -webkit-line-clamp: 1;
        /* number of lines to show */
        line-clamp: 1;
        -webkit-box-orient: vertical;
    }

    &.focus {
        background-color: rgb(255, 255, 255);
    }
}

.option {
    &.active {
        background: #4782ff;
        border-radius: 3px;
        color: #ffffff !important;
        pointer-events: none;
    }

    &.disabled {
        background: #ffffff;
        pointer-events: none;
    }
}

.select-box .option:hover {
    background: #687cc4;
    border-radius: 3px;
    color: #ffffff;
}

.search-box {
    width: 100%;
    padding: 6px;

    input {
        width: 100%;
        padding: 6px 6px;
        font-family: Roboto, sans-serif;
        font-size: 13px;
        background-color: #ffffff;
        border: 1px solid #c5c5c5;
        border-radius: 4px;
    }
}

.icon {
    position: absolute;
    right: 10px;
    top: 10px;
}

.active {
    .icon {
        transform: rotate(90deg);
        transition: all 0.1s;
    }
}

.box {
    position: relative;
    width: 100%;
    height: 40px;
    .disabled {
        // background-color: #e7e3e3 !important;
        pointer-events: none;

        .selected {
            background-color: rgb(230, 230, 230) !important;
            color: #ffffff !important;
        }
    }
}
::-webkit-scrollbar {
    width: 6px !important;
    height: 6px !important;
}
::-webkit-scrollbar-track {
    border-radius: 14px;
    background-color: var(--bs-white) !important;
}
::-webkit-scrollbar-thumb {
    background-color: #8592a38f !important;
    border-radius: 14px;
    &:hover {
        background-color: var(--bs-secondary) !important;
    }
}
</style>

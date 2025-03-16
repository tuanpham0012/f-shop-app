<template >
    <tr v-if="entry" @click="showChildren = !showChildren" class="table-row">
        <td class="text-center"><img :src="entry.image" class="max-w-[45px] max-h-[45px] object-fill m-auto" ></td>
        <td class="max-w-[350px] pb-0" >
            <div class="d-flex align-content-center" :style="{ paddingLeft: `${props.depth * 1.125}rem` }">
                <!-- <i class="fa-solid fa-l -translate-y-1.5" v-if="props.depth != 0"></i> <i class="fa-solid fa-window-minimize -translate-y-1" v-else></i>   -->
                <div v-if="entry.children && entry.children.length">
                    <i class="fa-solid fa-chevron-down translate-y-0.5 me-2" v-if="showChildren"></i>
                    <i class="fa-solid fa-chevron-right translate-y-0.5 me-2" v-else></i>
                </div>
                <div v-else>
                    <i class="fa-solid fa-minus translate-y-0.5 me-2"></i>
                </div>
                {{ entry.name }}
            </div>
            
        </td>
        <td>{{ entry.productCount }}</td>
        <td>{{ entry.notUse ? "Ngưng sử dụng" : "Hiển thị" }}</td>
        <td class="text-center"><i class="fa-regular fa-square" v-if="!entry.isPopular"></i>  <i class="fa-regular fa-square-check" v-else></i></td>
        <td class="text-center">
            <button
                type="button"
                class="btn btn-sm btn-icon btn-outline-primary me-1"
                @click="() => $emit('toggleEdit', entry.id)"
            >
                <span class="tf-icons bx bx-edit-alt bx-xs"></span>
            </button>
            <button
                type="button"
                class="btn btn-sm btn-icon btn-outline-secondary me-1"
                @click="() => $emit('toggleDelete', entry.id)"
            >
                <span class="tf-icons bx bx-trash-alt bx-xs"></span>
            </button>
        </td>
    </tr>
    <TableCategoryRow v-if="entry && showChildren" v-for="(item, index) in entry.children" :key="index" :entry="item" :depth="props.depth + 1" @toggle-edit="(id) => $emit('toggleEdit', id)"/>
</template>
<script setup lang="ts">
import { defineProps, defineEmits, ref } from 'vue';
import { viewFile } from '@/helpers/helpers';
const props = defineProps({
    entry: {
        type: Object,
        default: () => null
    },
    depth: {
        type: Number,
        default: 0
    }
});

defineEmits(['toggleEdit', 'toggleDelete']);

const showChildren = ref(true);
</script>
<style lang="scss" scoped>
.table-row{
    animation-duration:0.2s;
    animation-name: slidein;
}
@keyframes slidein {
  from {
    max-height: 0px;
    transform: scale(0);
    opacity: 0;
  }

  to {
    max-height: 100%;
    transform: scale(1);
    opacity: 1;
  }
}
</style>

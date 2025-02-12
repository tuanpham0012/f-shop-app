<template>
    <tr>
        <td class="max-w-[350px] pb-0" >
            <div class="d-flex align-content-center" :style="{ paddingLeft: `${props.depth * 1.125}rem` }">
                <!-- <i class="fa-solid fa-l -translate-y-1.5" v-if="props.depth != 0"></i> <i class="fa-solid fa-window-minimize -translate-y-1" v-else></i>   -->
                <i class="fa-solid fa-chevron-down translate-y-0.5 me-2" v-if="menu.children.length > 0"></i>
                <i class="fa-solid fa-chevron-right translate-y-0.5 me-2" v-else></i> {{ menu.title }}
            </div>
            
        </td>
        <td class="text-center"><div v-html="menu.icon"></div></td>
        <td>{{ menu.url }}</td>
        <td>{{ menu.hidden ? "Đã ẩn" : "Hiển thị" }}</td>
        <td class="text-center">
            <button
                type="button"
                class="btn btn-sm btn-icon btn-outline-primary me-1"
                @click="() => $emit('toggleEdit', menu.id)"
            >
                <span class="tf-icons bx bx-edit-alt bx-xs"></span>
            </button>
            <button
                type="button"
                class="btn btn-sm btn-icon btn-outline-secondary me-1"
            >
                <span class="tf-icons bx bx-trash-alt bx-xs"></span>
            </button>
        </td>
    </tr>
    <TableMenuRow v-for="(item, index) in menu.children" :key="index" :menu="item" :depth="props.depth + 1" @toggle-edit="(id) => $emit('toggleEdit', id)"/>
</template>
<script setup lang="ts">
const props = defineProps({
    menu: {
        type: Object,
        default: () => null
    },
    depth: {
        type: Number,
        default: 0
    }
});

defineEmits(['toggleEdit']);
</script>
<style lang=""></style>

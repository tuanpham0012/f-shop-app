<?php

namespace Database\Seeders;

use Illuminate\Support\Str;
use Illuminate\Database\Seeder;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Support\Facades\DB;

class ProductSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        $images = [
            '1746698535_ZZASC5KRIB1KR3QI.png',
            '1746698535_UMUUG7JDC7D15YS1.jpg',
            '1746698535_K5ICZ812JLT8WWLW.png',
            '1744943054_9Q50Q81LHR8DQWH9.png',
            '1744943054_1B9HOMMHKC84DOE8.png',
            '1744271208_D1MRYBH9KCFN9VMZ.jpg',
            '1744941991_V45YQU6C2RSQOC9N.png',
            '1744941991_URJBH1DQSE4WBQ17.png',
            '1744621537_CG0LKWS9TYEWFKQ3.png',
            '1744941991_L41W1UJL603529LC.png',
            '1744621537_BLXGXPROE9OPB2I5.png',
            '1744621537_B1A7RJEBOWIM8AQ5.png',
            '1744621537_42XLMFOEZMK28OGI.png',
            '1743480197_5O24OTQTDL3EDJNP.jpg',
        ];
        for ($i = 0; $i < 25; $i++) {
            $products = [];
            for ($j = 0; $j < 25; $j++) {
                $name = fake()->lastName . ' ' . fake()->firstName . ' ' . fake()->name;
                $products[] = [
                    'code' => Str::random(10),
                    'barcode' => (Str::random(17)),
                    'name' => $name,
                    'image_thumb' => $images[array_rand($images)],
                    'number_warning' => rand(20, 50),
                    'price' => rand(100000, 500000),
                    'unit_sell' => fake()->firstName,
                    'unit_buy' => fake()->lastName,
                    'alias' => Str::slug($name, '-'),
                    'category_id' => rand(1, 7),
                    'brand_id' => rand(1, 18),
                    'tax_id' => 1,
                    'is_new' => rand(0, 1),
                    'is_featured' => rand(0, 1),
                    'is_sale' => rand(0, 1),
                    // 'created_at' => now(),
                    // 'updated_at' => now(),
                ];
            }
            // dd($products);
            DB::table('products')->insert($products);
        }

        $attributes = [
            'Màu sắc' => ['Đỏ', 'Xanh', 'Vàng'],
            'Kích thước' => ['S', 'M'],
            'Chất liệu' => ['Cotton', 'Lụa']
        ];

        $getProducts = DB::table('products')->skip(1000)->take(15)->get();
        foreach ($getProducts as $product) {
            $randomKeys = array_rand($images, rand(5, 8));
            $randomProducts = array_intersect_key($images, array_flip($randomKeys));
            $productImg = [];
            foreach ($randomProducts as $image) {
                $productImg[] = [
                    'code' => Str::random(10),
                    'product_id' => $product->id,
                    'path' => $image,
                    'driver' => 'Local',
                    'extension' => pathinfo($image, PATHINFO_EXTENSION),
                    'file_name' => $image,
                    'created_at' => now(),
                    'updated_at' => now(),
                ];
            }
            DB::table('product_images')->insert($productImg);

            $this->generateVariants($product, $attributes);
        }
    }

    public function generateVariants(mixed $product, array $attributes): void
    {
        $attributes = [
            'Màu sắc' => ['Đỏ', 'Xanh', 'Vàng'],
            'Kích thước' => ['S', 'M'],
            'Chất liệu' => ['Cotton', 'Lụa']
        ];
        $randomKeys = array_rand($attributes, 2);
        $randomAttributes = array_intersect_key($attributes, array_flip($randomKeys));
        $optionIds = [];
        $optionValueIds = [];
        foreach ($randomAttributes as $key => $value) {
            $optionId = DB::table('options')->insertGetId([
                'code' => Str::random(15),
                'name' => $key,
                'product_id' => $product->id,
                'created_at' => now(),
            ]);
            $optionIds[$key] = $optionId;
            foreach ($value as $optionValue) {
                $optionValueIds[$optionValue] = DB::table('option_values')->insertGetId([
                    'code' => Str::random(15),
                    'value' => $optionValue,
                    'label' => $optionValue,
                    'option_id' => $optionId,
                    'product_id' => $product->id,
                    'created_at' => now(),
                ]);
            }
        }
        // Sử dụng hàm generate để tạo các biến thể sản phẩm
        $variants = $this->generate($randomAttributes);

        foreach ($variants as $variant) {
            $variant['product_id'] = $product->id;
            $variant['created_at'] = now();
            $variant['updated_at'] = now();
            $skuId = DB::table('skus')->insertGetId([
                'name' => $variant['name'],
                'product_id' => $product->id,
                'image_path' => $product->image_thumb,
                'barcode' => (Str::random(17)),
                'price' => rand($product->price, $product->price + 200000),
                'stock' => rand(100, 400)
            ]);
            // Lưu các thuộc tính của biến thể
            foreach ($variant['attributes'] as $attribute) {
                $optionId = $optionIds[$attribute['group']];
                $optionValueId = $optionValueIds[$attribute['value']];
                DB::table('variants')->insert([
                    'product_id' => $product->id,
                    'option_id' => $optionId,
                    'option_value_id' => $optionValueId,
                    'sku_id' => $skuId,
                ]);
            }
        }

        // dd($variants);
    }

    public function generate(array $attributes): array
    {
        // Nếu không có thuộc tính nào, trả về mảng rỗng
        if (empty($attributes)) {
            return [];
        }

        // Khởi tạo kết quả với một mảng rỗng bên trong. Đây là điểm bắt đầu.
        $result = [[]];

        // Lặp qua từng nhóm thuộc tính (ví dụ: 'Màu sắc', 'Kích thước')
        foreach ($attributes as $attributeName => $options) {
            // Mảng tạm để lưu các tổ hợp mới
            $tempResult = [];

            // Lặp qua các tổ hợp đã có trong $result
            foreach ($result as $combination) {
                // Lặp qua từng giá trị trong nhóm thuộc tính hiện tại (ví dụ: 'Đỏ', 'Xanh')
                foreach ($options as $optionValue) {
                    // Tạo một tổ hợp mới bằng cách thêm giá trị hiện tại vào tổ hợp cũ.
                    // Chúng ta sẽ lưu cả tên thuộc tính và giá trị của nó.
                    $newCombination = $combination;
                    $newCombination[] = [
                        'group' => $attributeName,
                        'value' => $optionValue,
                    ];
                    $tempResult[] = $newCombination;
                }
            }
            // Cập nhật $result bằng các tổ hợp mới vừa tạo ra
            $result = $tempResult;
        }

        // Định dạng lại đầu ra cho dễ sử dụng
        return $this->formatResult($result);
    }

    /**
     * Định dạng lại mảng kết quả cuối cùng.
     *
     * @param array $combinations
     * @return array
     */
    protected function formatResult(array $combinations): array
    {
        $variants = [];

        foreach ($combinations as $combination) {
            if (empty($combination)) continue;
            
            $variantNameParts = array_column($combination, 'value');
            $variantName = implode(' | ', $variantNameParts);

            // Tạo SKU (Stock Keeping Unit) tự động (tùy chọn)
            $sku = implode('-', array_map('strtoupper', $variantNameParts));

            $variants[] = [
                'name' => $variantName, // Ví dụ: "Đỏ - S - Cotton"
                'sku' => $sku,         // Ví dụ: "DO-S-COTTON"
                'attributes' => $combination,
                // Bạn có thể thêm các trường mặc định khác ở đây
                'price' => 0,
                'stock' => 0,
            ];
        }

        return $variants;
    }
}

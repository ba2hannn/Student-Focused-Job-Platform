import os
import xml.etree.ElementTree as ET

# XML dosyalarının bulunduğu dizin
xml_dir = r"C:\Users\batuh\OneDrive\Masaüstü\deneme\a\\"

# XML dosyalarını işle
for filename in os.listdir(xml_dir):
    if filename.endswith('.xml'):
        xml_path = os.path.join(xml_dir, filename)

        # XML dosyasını oku
        tree = ET.parse(xml_path)
        root = tree.getroot()

        # Her bir nesne etiketini kontrol et
        for obj in root.findall('object'):
            name = obj.find('name')
            if name.text == 'Pine':
                # Etiketi güncelle
                name.text = 'pine'
            else:
                # Gereksiz etiketleri sil
                root.remove(obj)

        # Güncellenmiş XML dosyasını kaydet
        updated_xml_path = os.path.join(xml_dir, 'updated_' + filename)
        tree.write(updated_xml_path)

        print(f"{xml_path} dosyası güncellendi.")

print("Tüm XML dosyaları işlendi ve güncellenmiş XML dosyaları kaydedildi.")

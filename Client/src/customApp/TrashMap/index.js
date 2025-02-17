import React from "react";
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet";
import L from "leaflet";
import "leaflet/dist/leaflet.css";
import MapWrapper from "./index.styled";

// 🗑️ Tạo icon thùng rác
const trashIcon = new L.Icon({
  iconUrl: "https://cdn-icons-png.flaticon.com/512/561/561127.png", // Thay bằng icon tùy ý
  iconSize: [32, 32],
  iconAnchor: [16, 32],
  popupAnchor: [0, -32],
});

// 📌 Danh sách tọa độ các thùng rác
const trashBins = [
  { id: 1, position: [10.9, 106.9], status: "80%" },
  { id: 2, position: [10.92, 106.91], status: "100%" },
  { id: 3, position: [10.88, 106.95], status: "50%" },
];

export default function TrashMap() {
  return (
    <MapWrapper className="map-container">
      <MapContainer
        center={[10.9, 106.9]}
        zoom={22}
        // style={{ height: "500px", width: "100%" }}
        className="fullmap"
      >
        {/* Lớp bản đồ */}
        {/* <TileLayer
          url="https://mt1.google.com/vt/lyrs=s&x={x}&y={y}&z={z}"
          attribution="© Google Maps"
        /> */}
        {/* <TileLayer
          url="https://{s}.basemaps.cartocdn.com/light_all/{z}/{x}/{y}{r}.png"
          attribution="© Carto"
        /> */}
        <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />

        {/* 🗑️ Render các thùng rác */}
        {trashBins.map((bin) => (
          <Marker key={bin.id} position={bin.position} icon={trashIcon}>
            <Popup>Thùng rác đầy: {bin.status}</Popup>
          </Marker>
        ))}
      </MapContainer>
    </MapWrapper>
  );
}

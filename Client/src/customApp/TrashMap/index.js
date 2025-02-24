import React, { useEffect, useState, useRef } from "react";
import * as signalR from "@microsoft/signalr";
import { TransformWrapper, TransformComponent } from "react-zoom-pan-pinch";
import { Tooltip } from "antd";
import TrashMap from "../img/So_do_tham_quan_chua_Tam_Chuc.jpg";
import happy from "../img/grinning-face.png";
import angry from "../img/angry.png";
import sad from "../img/sad.png";
import emotion from "../img/emoji.png";
const getEmojiData = (value) => {
  switch (value) {
    case 1:
      return { src: happy, title: "vui vẻ" };
    case 2:
      return { src: angry, title: "Tức giận" };
    case 3:
      return { src: sad, title: "Buồn bã" };
    case 4:
      return { src: emotion, title: "Bình thường" };
    default:
      return { src: happy, title: "Vui vẻ" };
  }
};

const ZoomableImage = () => {
  const [typeEmotion1, settypeEmotion1] = useState(null);
  const [typeEmotion2, settypeEmotion2] = useState(null);

  console.log(typeEmotion1);
  console.log(typeEmotion2);

  const a = typeEmotion1;
  const b = 2;
  const c = typeEmotion2;
  const d = 2;
  const e = 1;
  const f = 3;
  const g = typeEmotion2;
  const h = 2;
  const i = 3;
  const k = 1;
  const smileyPositions = [
    { top: "88%", left: "24%", ...getEmojiData(a) },
    { top: "70%", left: "63%", ...getEmojiData(b) },
    { top: "60%", left: "75%", ...getEmojiData(c) },
    { top: "50%", left: "60%", ...getEmojiData(d) },
    { top: "10%", left: "85%", ...getEmojiData(e) },
    { top: "30%", left: "44%", ...getEmojiData(f) },
    { top: "20%", left: "30%", ...getEmojiData(g) },
    { top: "15%", left: "20%", ...getEmojiData(h) },
    { top: "53%", left: "32%", ...getEmojiData(i) },
    { top: "55%", left: "42%", ...getEmojiData(k) },
  ];
  const [message, setMessage] = useState("");
  const [connectionStatus, setConnectionStatus] = useState("Disconnected");
  const reconnectInterval = 10000;
  const [temperature, setTemperature] = useState("");
  const connectionRef = useRef(null);
  const reconnectTimerRef = useRef(null);
  useEffect(() => {
    fetch("https://nx-emotional.gosol.com.vn/api/v1/NxEmotion/Data", {
      method: "GET",
    })
      .then((response) => response.json())
      .then((result) => {
        if (result.Status === 1) {
          settypeEmotion1(result.Data[0].TypeEmotion);
          settypeEmotion2(result.Data[1].TypeEmotion);
        } else {
          console.error("Error fetching data:", result.Message);
        }
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  }, []);
  useEffect(() => {
    // Hàm kết nối lại SignalR
    const startConnection = async () => {
      if (connectionRef.current) return; // Nếu kết nối đã tồn tại thì không khởi tạo lại

      connectionRef.current = new signalR.HubConnectionBuilder()
        .withUrl("https://nx-emotional.gosol.com.vn/emotion", {
          withCredentials: false,
        })
        .build();

      connectionRef.current.on("Flag", async (data) => {
        setMessage(JSON.stringify(data));
        if (data.flag === true) {
          try {
            if (
              connectionRef.current?.state ===
              signalR.HubConnectionState.Connected
            ) {
              const response = await connectionRef.current.invoke("Data");
              settypeEmotion1(response[0].typeEmotion);
              settypeEmotion2(response[1].typeEmotion);
            } else {
              console.warn(
                "Cannot call Data, SignalR connection is not active."
              );
            }
          } catch (error) {
            console.error("Error invoking Data:", error);
          }
        }
      });

      connectionRef.current.onclose(() => {
        setConnectionStatus("Disconnected");
        console.log("SignalR connection closed. Reconnecting...");
        scheduleReconnect();
      });

      try {
        await connectionRef.current.start();
        setConnectionStatus("Connected");
        await connectionRef.current.invoke("RegisterDevice");
      } catch (err) {
        console.error("SignalR connection error:", err);
        scheduleReconnect();
      }
    };

    // Hàm thử kết nối lại sau thời gian
    const scheduleReconnect = () => {
      if (reconnectTimerRef.current) clearTimeout(reconnectTimerRef.current);
      reconnectTimerRef.current = setTimeout(() => {
        startConnection();
      }, reconnectInterval);
    };

    startConnection();

    return () => {
      if (reconnectTimerRef.current) clearTimeout(reconnectTimerRef.current);
      if (connectionRef.current) {
        connectionRef.current.stop();
        connectionRef.current = null;
      }
    };
  }, []);

  return (
    <div
      className="zoom-container"
      style={{
        overflow: "hidden",
        width: "100%",
        height: "100%",
        position: "relative",
      }}
    >
      <TransformWrapper
        initialScale={1}
        alignmentAnimation={{ sizeX: 0, sizeY: 0 }}
        minScale={1}
        maxScale={5}
        wheel={{ step: 0.1 }}
        limitToBounds={true}
        panning={{ disabled: false }}
        doubleClick={{ disabled: true }}
        pinch={{ disabled: false }}
        centerZoomedOut={true}
      >
        <TransformComponent>
          <div
            style={{ position: "relative", width: "100vw", height: "100vh" }}
          >
            <img
              src={TrashMap}
              alt="Zoomable"
              style={{ width: "100%", height: "100%", display: "block" }}
            />
            {smileyPositions.map((pos, index) => (
              <Tooltip
                title={pos.title}
                overlayStyle={{ zIndex: 9999 }}
                color="blue"
                key={index}
              >
                <div
                  style={{
                    position: "absolute",
                    top: pos.top,
                    left: pos.left,
                    cursor: "pointer",
                    zIndex: 10,
                    width: "50px",
                    height: "50px",
                    transition: "transform 0.3s, filter 0.3s", 
                    background: "white",
                    borderRadius:"50%",
                    padding:"5px"
                  }}
                  onMouseEnter={(e) => {
                    e.currentTarget.style.transform = "scale(1.2) rotate(10deg)"; 
                    e.currentTarget.style.filter = "brightness(1.2)";
                  }} 
                  onMouseLeave={(e) => {
                    e.currentTarget.style.transform = "scale(1) rotate(0deg)"; 
                    e.currentTarget.style.filter = "brightness(1)";
                  }} 
                >
                  <img
                    src={pos.src}
                    alt={pos.title}
                    style={{
                      width: "40px",
                      height: "40px",
                    }}
                  />
                </div>
              </Tooltip>
            ))}
          </div>
        </TransformComponent>
      </TransformWrapper>
    </div>
  );
};

export default ZoomableImage;

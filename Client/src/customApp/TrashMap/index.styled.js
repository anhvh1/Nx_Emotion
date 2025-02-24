// import styled from "styled-components";

// const MapWrapper = styled.div`
//   height: "100vh";
//   .fullmap {
//     height: 100vh;
//   }
// `;

// export default MapWrapper;
import styled from 'styled-components';

export const Wrapper = styled.div`
  width: 100%;
  height: 100vh; /* Full screen height */
  display: flex;
  justify-content: center;
  align-items: center;
`;

export const Image = styled.img`
  width: 100%;
  height: 100%;
  object-fit: contain; /* Ensures the image does not stretch */
`;

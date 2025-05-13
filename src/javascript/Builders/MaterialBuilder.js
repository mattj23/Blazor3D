import * as THREE from "three";
import TextureBuilder from "./TextureBuilder";

class MaterialBuilder {
  static buildMaterial(options) {
    if (options.type == "MeshStandardMaterial") {

      let map = TextureBuilder.buildTexture(options.map);
      
      const material = new THREE.MeshStandardMaterial({
        color: options.color,
        transparent : options.transparent,
        opacity : options.opacity,
        flatShading : options.flatShading,
        metalness: options.metalness,
        roughness: options.roughness,
        wireframe: options.wireframe,
        map: map,
        depthTest: options.depthTest,
        depthWrite: options.depthWrite
      });
      material.uuid = options.uuid;
      return material;
    }

    if (options.type == "LineBasicMaterial") {

      const material = new THREE.LineBasicMaterial({
        color: options.color,
        transparent : options.transparent,
        opacity : options.opacity,
        linecap : options.lineCap,
        linejoin : options.lineJoin,
        linewidth : options.lineWidth,
        depthTest: options.depthTest,
        depthWrite: options.depthWrite
      });
      material.uuid = options.uuid;
      return material;
    }

    if(options.type == "SpriteMaterial") {
      let map = TextureBuilder.buildTexture(options.map);
      const material = new THREE.SpriteMaterial({
        isSpriteMaterial:true,
        color: options.color,
        transparent : options.transparent,
        opacity : options.opacity,
        map: map,
        rotation: options.rotation,
        depthTest: options.depthTest,
        depthWrite: options.depthWrite
      })
      material.uuid = options.uuid;
      return material;
    }

    if (options.type == "ShaderMaterial") {
      const material = new THREE.ShaderMaterial({
        fragmentShader: options.fragmentShader,
        vertexShader: options.vertexShader || `
          void main() {
            gl_Position = projectionMatrix * modelViewMatrix * vec4(position, 1.0);
          }
        `,
        uniforms: Object.fromEntries(Object.entries(options.uniforms || {}).map(([k, v]) => [k, {value: v}])),
        transparent: options.transparent,
        opacity: options.opacity,
        depthTest: options.depthTest,
        depthWrite: options.depthWrite
      });
      material.uuid = options.uuid;
      return material;
    }

  }
}

export default MaterialBuilder;

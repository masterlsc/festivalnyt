using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class CameraValues
    {
	    public CameraClearFlags clearFlags = CameraClearFlags.Skybox;
        public bool clearFlagsEnabled;
    
        public Color backgroundColor;
        public bool backgroundColorEnabled;
    
        public LayerMask cullingMask;
        public bool cullingMaskEnabled;
    
        public bool orthographic;
        public bool orthographicEnabled;
    
        public float orthographicSize;
        public bool orthographicSizeEnabled;
    
        public float fieldOfView;
        public bool fieldOfViewEnabled;
    
        public float farClipPlane;
        public bool farClipPlaneEnabled;
    
        public float nearClipPlane;
        public bool nearClipPlaneEnabled;
    
	    public Rect rect = new Rect();
        public bool rectEnabled;
    
        public float depth;
        public bool depthEnabled;
    
	    public RenderingPath renderingPath = RenderingPath.UsePlayerSettings;
        public bool renderingPathEnabled;
    
        public RenderTexture targetTexture;
        public bool targetTextureEnabled;
    
        public bool useOcclusionCulling;
        public bool useOcclusionCullingEnabled;
    
        public bool hdr;
        public bool hdrEnabled;
    
        
        public CameraValues CloneValues ()
        {
            CameraValues values = new CameraValues();
            
            values.clearFlags = this.clearFlags;
            values.clearFlagsEnabled = this.clearFlagsEnabled;
            
            values.backgroundColor = this.backgroundColor;
            values.backgroundColorEnabled = this.backgroundColorEnabled;
            
            values.cullingMask = this.cullingMask;
            values.cullingMaskEnabled = this.cullingMaskEnabled;
            
            values.orthographic = this.orthographic;
            values.orthographicEnabled = this.orthographicEnabled;
            
            values.orthographicSize = this.orthographicSize;
            values.orthographicSizeEnabled = this.orthographicSizeEnabled;
            
            values.fieldOfView = this.fieldOfView;
            values.fieldOfViewEnabled = this.fieldOfViewEnabled;
            
            values.farClipPlane = this.farClipPlane;
            values.farClipPlaneEnabled = this.farClipPlaneEnabled;
            
            values.nearClipPlane = this.nearClipPlane;
            values.nearClipPlaneEnabled = this.nearClipPlaneEnabled;
            
            values.rect = this.rect;
            values.rectEnabled = this.rectEnabled;
            
            values.depth = this.depth;
            values.depthEnabled = this.depthEnabled;
            
            values.renderingPath = this.renderingPath;
            values.renderingPathEnabled = this.renderingPathEnabled;
            
            values.targetTexture = this.targetTexture;
            values.targetTextureEnabled = this.targetTextureEnabled;
            
            values.useOcclusionCulling = this.useOcclusionCulling;
            values.useOcclusionCullingEnabled = this.useOcclusionCullingEnabled;
            
            values.hdr = this.hdr;
            values.hdrEnabled = this.hdrEnabled;
            
            return values;
        }
    }
}

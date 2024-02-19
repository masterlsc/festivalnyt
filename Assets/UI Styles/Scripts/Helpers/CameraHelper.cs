using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class CameraHelper
    {
        /// <summary>
        /// Create Style, pass in an object to match the styles values with the objects values
        /// </summary>
        public static void CreateStyle(StyleDataFile data, string category, string styleName, string findByName, GameObject obj)
        {
            Style style = new Style(data, category, styleName, findByName);
            style.rename = !Application.isPlaying;

            StyleComponent v = new StyleComponent(data, style, StyleComponentType.Camera, "Camera", "");

            if (obj != null)
                v.camera = SetValuesFromComponent(obj);

            UIStylesDatabase.Save();
        }

        /// <summary>
        /// Sets the Values from a component
        /// </summary>
        public static CameraValues SetValuesFromComponent(Component com)
        {
            if (com is Camera)
                return SetValuesFromComponent((Camera)com, null);

            else Debug.LogError("SetValuesFromComponent: Type is: " + com.GetType() + " but should be Camera");

            return null;
        }
        public static CameraValues SetValuesFromComponent(GameObject obj)
        {
            if (obj.GetComponent<Camera>())
                return SetValuesFromComponent(obj.GetComponent<Camera>(), obj);

            else Debug.LogError("SetValuesFromComponent: Component not found!");

            return null;
        }

        public static CameraValues SetValuesFromComponent(Camera value, GameObject obj)
        {
            CameraValues values = new CameraValues();

            values.clearFlags = value.clearFlags;
            values.clearFlagsEnabled = true;

            values.backgroundColor = value.backgroundColor;
            values.backgroundColorEnabled = true;

            values.cullingMask = value.cullingMask;
            values.cullingMaskEnabled = true;

            values.orthographic = value.orthographic;
            values.orthographicEnabled = true;

            values.orthographicSize = value.orthographicSize;
            values.orthographicSizeEnabled = true;

            values.fieldOfView = value.fieldOfView;
            values.fieldOfViewEnabled = true;

            values.farClipPlane = value.farClipPlane;
            values.farClipPlaneEnabled = true;

            values.nearClipPlane = value.nearClipPlane;
            values.nearClipPlaneEnabled = true;

            values.rect = value.rect;
            values.rectEnabled = true;

            values.depth = value.depth;
            values.depthEnabled = true;

            values.renderingPath = value.renderingPath;
            values.renderingPathEnabled = true;

            values.targetTexture = value.targetTexture;
            values.targetTextureEnabled = true;

            values.useOcclusionCulling = value.useOcclusionCulling;
            values.useOcclusionCullingEnabled = true;

            values.hdr = value.allowHDR;
            values.hdrEnabled = true;

            return values;
        }

        /// <summary>
        /// Apply
        /// </summary>
        public static void Apply(CameraValues values, GameObject obj)
        {
            if (obj.GetComponent<Camera>())
            {
                Camera component = obj.GetComponent<Camera>();

                if (values.clearFlagsEnabled)
                    component.clearFlags = values.clearFlags;

                if (values.backgroundColorEnabled)
                    component.backgroundColor = values.backgroundColor;

                if (values.cullingMaskEnabled)
                    component.cullingMask = values.cullingMask;

                if (values.orthographicEnabled)
                    component.orthographic = values.orthographic;

                if (values.orthographicSizeEnabled)
                    component.orthographicSize = values.orthographicSize;

                if (values.fieldOfViewEnabled)
                    component.fieldOfView = values.fieldOfView;

                if (values.farClipPlaneEnabled)
                    component.farClipPlane = values.farClipPlane;

                if (values.nearClipPlaneEnabled)
                    component.nearClipPlane = values.nearClipPlane;

                if (values.rectEnabled)
                    component.rect = values.rect;

                if (values.depthEnabled)
                    component.depth = values.depth;

                if (values.renderingPathEnabled)
                    component.renderingPath = values.renderingPath;

                if (values.targetTextureEnabled)
                    component.targetTexture = values.targetTexture;

                if (values.useOcclusionCullingEnabled)
                    component.useOcclusionCulling = values.useOcclusionCulling;

                if (values.hdrEnabled)
                    component.allowHDR = values.hdr;

            }
        }
    }
}
